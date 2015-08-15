IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AddEventFilter]') AND type in (N'P', N'PC'))
DROP PROC dbo.usp_AddEventFilter
GO
CREATE PROC dbo.usp_AddEventFilter
(
	@EventFilter NVARCHAR(MAX),
	@Guid NVARCHAR(MAX) OUT
)
AS 
BEGIN
DECLARE @EventGuid NVARCHAR(MAX)
SELECT @EventGuid = Id FROM EventFilter WHERE EventFilterInXml = @EventFilter;

IF ISNULL(@EventGuid, '') = ''
	BEGIN
		INSERT INTO EventFilter (FilterGuid, EventFilterInXml, CreatedDateTime, LastUpdateDateTime, IsDeleted)
						 VALUES (@Guid, @EventFilter, GETUTCDATE(), GETUTCDATE(), 0)
						 
		SET @Guid = CONVERT(nvarchar(10), scope_identity());
	END
ELSE
	BEGIN
		SET @Guid = @EventGuid
	END
END