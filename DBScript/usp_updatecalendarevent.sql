Create Proc Usp_UpdateCalendarEvents
(
@newValue Nvarchar(max),
@oldValue Nvarchar(max),
@purpose Int
)
AS
BEGIN

IF(@purpose = 1)
BEGIN
Update CalendarEvent Set EventGroupName = @newValue Where EventGroupName = @oldValue;
END
IF(@purpose = 2)
BEGIN
Update CalendarEvent Set EventGroupTitle = @newValue Where EventGroupTitle = @oldValue;
END
IF(@purpose = 3)
BEGIN
Update CalendarEvent Set EventTypeOne = @newValue Where EventTypeOne = @oldValue;
END

END