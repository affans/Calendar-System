
-- Starting EventGroupTitle
IF NOT EXISTS (SELECT Title FROM EventGroupTitle WHERE Title = 'Department')
BEGIN
	INSERT INTO EventGroupTitle (Title,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Department',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT Title FROM EventGroupTitle WHERE Title = 'Club')
BEGIN
	INSERT INTO EventGroupTitle (Title,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Club',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT Title FROM EventGroupTitle WHERE Title = 'Union')
BEGIN
	INSERT INTO EventGroupTitle (Title,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Union',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT Title FROM EventGroupTitle WHERE Title = 'Athletic')
BEGIN
	INSERT INTO EventGroupTitle (Title,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Athletic',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT Title FROM EventGroupTitle WHERE Title = 'Library')
BEGIN
	INSERT INTO EventGroupTitle (Title,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Library',GETUTCDATE(),GETUTCDATE(),0)
END

 -- Starting EventGroupName
IF NOT EXISTS (SELECT GroupName FROM EventGroupName WHERE GroupName = 'Department of Math')
BEGIN
	INSERT INTO EventGroupName (GroupName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Department of Math',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT GroupName FROM EventGroupName WHERE GroupName = 'Department of Physics')
BEGIN
	INSERT INTO EventGroupName (GroupName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Department of Physics',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT GroupName FROM EventGroupName WHERE GroupName = 'Department of Biology')
BEGIN
	INSERT INTO EventGroupName (GroupName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Department of Biology',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT GroupName FROM EventGroupName WHERE GroupName = 'Math Club')
BEGIN
	INSERT INTO EventGroupName (GroupName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Math Club',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT GroupName FROM EventGroupName WHERE GroupName = 'Biology Club')
BEGIN
	INSERT INTO EventGroupName (GroupName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Biology Club',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT GroupName FROM EventGroupName WHERE GroupName = 'Physics Club')
BEGIN
	INSERT INTO EventGroupName (GroupName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Physics Club',GETUTCDATE(),GETUTCDATE(),0)
END

-- Starting EventTypeOne
IF NOT EXISTS (SELECT EventTypeName FROM EventTypeOne WHERE EventTypeName = 'Food')
BEGIN
	INSERT INTO EventTypeOne (EventTypeName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Food',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT EventTypeName FROM EventTypeOne WHERE EventTypeName = 'Movie Night')
BEGIN
	INSERT INTO EventTypeOne (EventTypeName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Movie Night',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT EventTypeName FROM EventTypeOne WHERE EventTypeName = 'Lecture')
BEGIN
	INSERT INTO EventTypeOne (EventTypeName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Lecture',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT EventTypeName FROM EventTypeOne WHERE EventTypeName = 'Colloquium')
BEGIN
	INSERT INTO EventTypeOne (EventTypeName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Colloquium',GETUTCDATE(),GETUTCDATE(),0)
END

IF NOT EXISTS (SELECT EventTypeName FROM EventTypeOne WHERE EventTypeName = 'Intramural')
BEGIN
	INSERT INTO EventTypeOne (EventTypeName,CreatedDateTime,LastUpdateDateTime,IsDeleted)
	VALUES ('Intramural',GETUTCDATE(),GETUTCDATE(),0)
END