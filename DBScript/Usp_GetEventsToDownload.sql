IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetEventsByGuid]') AND type in (N'P', N'PC'))
DROP PROC dbo.usp_GetEventsByGuid
GO
CREATE PROC dbo.usp_GetEventsByGuid
(
	@Guid NVARCHAR(max) 
)
AS 
BEGIN
DECLARE @EventFilterXml AS XML;
DECLARE @StartDate DateTime;
DECLARE @EndDate DateTime;
SELECT @EventFilterXml = CAST (EventFilterInXml AS XML) FROM EventFilter WHERE Id = @Guid;

SELECT @StartDate = @EventFilterXml.value('(/EventFilter/StartDate/node())[1]', 'DateTime');
SELECT @EndDate =  @EventFilterXml.value('(/EventFilter/EndDate/node())[1]', 'DateTime');

WITH EventFilter_Group (EventGroupTitle)
	AS
	(   							
		SELECT  t.gr.value('.', 'nvarchar(max)') AS EventGroupTitles
		FROM @EventFilterXml.nodes('/EventFilter/Groups/GroupTitle') t(gr)
	),
	EventFilter_GroupName (EventGroupName)
	AS
	(   							
		SELECT  t.gr.value('.', 'nvarchar(max)') AS EventGroupNames
		FROM @EventFilterXml.nodes('/EventFilter/Groups/GroupName') t(gr)
	),
	 EventFilter_Event (EventTypeOne)
	AS
	(   							
		SELECT  r.ty.value('.', 'nvarchar(max)') AS EventTypesOne
		FROM @EventFilterXml.nodes('/EventFilter/EventTypes/EventTypeOne') r(ty)
	)
	SELECT ce.* FROM CalendarEvent ce
	INNER JOIN EventFilter_Group gp ON
				CASE gp.EventGroupTitle WHEN 'All' THEN ce.EventGroupTitle
				ELSE gp.EventGroupTitle
				END = ce.EventGroupTitle
	INNER JOIN EventFilter_GroupName gpName ON
				CASE gpName.EventGroupName WHEN 'All' THEN ce.EventGroupName
				ELSE gpName.EventGroupName
				END = ce.EventGroupName
	INNER JOIN EventFilter_Event et ON
				CASE et.EventTypeOne WHEN 'All' THEN  ce.EventTypeOne
				ELSE et.EventTypeOne
				END = ce.EventTypeOne
	WHERE ce.IsDeleted = 0 
		AND (ce.StartDate >= CASE WHEN ISNULL(@StartDate,'') <> '' THEN  @StartDate ELSE ce.StartDate END)
		AND (ce.EndDate <= CASE WHEN ISNULL(@EndDate,'') <> '' THEN  @EndDate ELSE ce.EndDate END);
END