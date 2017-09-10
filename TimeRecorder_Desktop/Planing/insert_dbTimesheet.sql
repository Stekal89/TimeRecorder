USE dbTimeSheet
GO


INSERT INTO tblPermission
VALUES
	('User'),
	('Administrator'),
	('Account department')
GO

SELECT * FROM tblPermission
GO

/*

	IDUser INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FKPermission INT FOREIGN KEY REFERENCES tblPermission NOT NULL,
	Firstname NVARCHAR(100) NOT NULL,
	Lastname NVARCHAR(100) NOT NULL,
	UserShortcut NVARCHAR(10),
	Street NVARCHAR(150) NOT NULL,
	Number VARCHAR(10) NOT NULL,
	Stage VARCHAR(10),
	Door VARCHAR(10),
	Place VARCHAR(100) NOT NULL,
	ZIPCode VARCHAR(10) NOT NULL,
	Department NVARCHAR(100) NOT NULL,
	Email NVARCHAR(MAX) 

*/

INSERT INTO tblUser (FKPermission, Firstname, Lastname, UserShortcut, Street, Number, Place, ZIPCode, Department, Email)
VALUES
	(1, 'Jonny', 'Johnson', 'jojo', 'Hauptstraße', '75A', 'Wasenbruck an der Leitha', '2452', 'Development', 'Jonny.Johnson@email.com'),
	(2, 'Herta', 'Mayer', 'hema', 'Siemensstraße', '113', 'Wien', '1210', 'Account department', 'Herta.Mayer@email.com'),
	(3, 'Paul', 'Jürgens', 'pajue', 'Am Hinterweg', '24', 'Hainburg an der Donau', '2410', 'Administrator', 'Paul.Juergens@email.com')
GO

SELECT * FROM tblUser
GO

-- How to convert a hashed value into a varchar
-- https://stackoverflow.com/questions/2120/convert-hashbytes-to-varchar

CREATE PROCEDURE pInsertPassword @userID INT, @password NVARCHAR(255)
AS
	DECLARE @convertedPassword VARCHAR(300)
	SET @convertedPassword = CAST(@password AS VARCHAR(500))

	DECLARE @pwd NVARCHAR(100)
	SET @pwd = master.dbo.fn_varbintohexsubstring(0, HashBytes('SHA1', @convertedPassword), 1, 0)

	-- SELECT @pwd AS OutVPass
	
	INSERT INTO tblLogin
	VALUES
		(@userID, @pwd, 0)
GO

EXEC pInsertPassword 1, '123User!'
GO
EXEC pInsertPassword 2, '123Admin!'
GO
EXEC pInsertPassword 3, '123AccDepartment!'
GO

SELECT * FROM tblLogin
GO

/*

	IDTime INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FKUser INT FOREIGN KEY REFERENCES tblUser NOT NULL,
	StartTime DATETIME DEFAULT GETDATE() NOT NULL,
	EndTime DATETIME,
	[Description] NVARCHAR(255),
	State VARCHAR(30) NOT NULL,
	TotalTime FLOAT

*/

CREATE PROC pInsertTime @userID INT, @startTime DATETIME, @endTime DATETIME, @Description NVARCHAR(255), @state VARCHAR(30) 
AS
	/*
	declare @startTime datetime
	declare @endTime datetime
	declare @total int
	
	set @startTime = '2017-09-04 08:00:0'
	set @endTime = '2017-09-04 12:30:0'
	*/
	DECLARE @totalMinute FLOAT = DATEDIFF(MINUTE, @startTime,@endTime)
	DECLARE @totalTime float = CONVERT(float, @totalMinute)
	
	SET @totalTime = @totalTime / 60
		
	INSERT INTO tblTime
	VALUES
	( @userID, @startTime, @endTime, @Description, @state, @totalTime)
GO

-- 5 days for User 1
EXEC pInsertTime 1,'2017-09-04 08:00:0','2017-09-04 12:30:0','I was working!','Working'
GO
EXEC pInsertTime 1,'2017-09-04 12:30:0','2017-09-04 13:00:0','I was eating!','Pause'
GO
EXEC pInsertTime 1,'2017-09-04 13:00:0','2017-09-04 16:30:0','I was working!','Working'
GO
-- 2
EXEC pInsertTime 1,'2017-09-05 08:30:0','2017-09-05 13:00:0','I was working!','Working'
GO
EXEC pInsertTime 1,'2017-09-05 13:00:0','2017-09-05 13:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 1,'2017-09-05 13:30:0','2017-09-05 17:30:0','I was working!','Working'
GO
-- 3
EXEC pInsertTime 1,'2017-09-06 08:00:0','2017-09-06 13:00:0','I was working!','Working'
GO
EXEC pInsertTime 1,'2017-09-06 13:00:0','2017-09-06 13:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 1,'2017-09-06 13:30:0','2017-09-06 17:30:0','I was working!','Working'
GO
-- 4
EXEC pInsertTime 1,'2017-09-07 08:00:0','2017-09-07 12:30:0','I was working!','Working'
GO
EXEC pInsertTime 1,'2017-09-07 12:30:0','2017-09-07 13:00:0','I was eating!','Pause'
GO
EXEC pInsertTime 1,'2017-09-07 13:00:0','2017-09-07 17:30:0','I was working!','Working'
GO
-- 5 
EXEC pInsertTime 1,'2017-09-08 08:00:0','2017-09-08 13:00:0','I was working!','Working'
GO
EXEC pInsertTime 1,'2017-09-08 13:00:0','2017-09-08 13:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 1,'2017-09-08 13:30:0','2017-09-08 17:30:0','I was working!','Working'
GO

-- ###########################################################################################

-- 5 days for User 2
EXEC pInsertTime 2,'2017-09-04 09:00:0','2017-09-04 13:30:0','I was working!','Working'
GO
EXEC pInsertTime 2,'2017-09-04 13:30:0','2017-09-04 14:00:0','I was eating!','Pause'
GO
EXEC pInsertTime 2,'2017-09-04 14:00:0','2017-09-04 16:30:0','I was working!','Working'
GO
-- 2
EXEC pInsertTime 2,'2017-09-05 09:30:0','2017-09-05 14:00:0','I was working!','Working'
GO
EXEC pInsertTime 2,'2017-09-05 14:00:0','2017-09-05 14:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 2,'2017-09-05 14:30:0','2017-09-05 18:30:0','I was working!','Working'
GO
-- 3
EXEC pInsertTime 2,'2017-09-06 09:00:0','2017-09-06 14:00:0','I was working!','Working'
GO
EXEC pInsertTime 2,'2017-09-06 14:00:0','2017-09-06 14:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 2,'2017-09-06 14:30:0','2017-09-06 18:30:0','I was working!','Working'
GO
-- 4
EXEC pInsertTime 2,'2017-09-07 09:00:0','2017-09-07 13:30:0','I was working!','Working'
GO
EXEC pInsertTime 2,'2017-09-07 13:30:0','2017-09-07 14:00:0','I was eating!','Pause'
GO
EXEC pInsertTime 2,'2017-09-07 14:00:0','2017-09-07 18:30:0','I was working!','Working'
GO
-- 5 
EXEC pInsertTime 2,'2017-09-08 09:00:0','2017-09-08 14:00:0','I was working!','Working'
GO
EXEC pInsertTime 2,'2017-09-08 14:00:0','2017-09-08 14:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 2,'2017-09-08 14:30:0','2017-09-08 18:30:0','I was working!','Working'
GO

-- ###########################################################################################

-- 5 days for User 3
EXEC pInsertTime 3,'2017-09-04 07:00:0','2017-09-04 12:30:0','I was working!','Working'
GO
EXEC pInsertTime 3,'2017-09-04 12:30:0','2017-09-04 13:00:0','I was eating!','Pause'
GO
EXEC pInsertTime 3,'2017-09-04 13:00:0','2017-09-04 16:30:0','I was working!','Working'
GO
-- 2
EXEC pInsertTime 3,'2017-09-05 07:30:0','2017-09-05 13:00:0','I was working!','Working'
GO
EXEC pInsertTime 3,'2017-09-05 13:00:0','2017-09-05 13:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 3,'2017-09-05 13:30:0','2017-09-05 17:30:0','I was working!','Working'
GO
-- 3
EXEC pInsertTime 3,'2017-09-06 07:00:0','2017-09-06 13:00:0','I was working!','Working'
GO
EXEC pInsertTime 3,'2017-09-06 13:00:0','2017-09-06 13:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 3,'2017-09-06 13:30:0','2017-09-06 17:30:0','I was working!','Working'
GO
-- 4
EXEC pInsertTime 3,'2017-09-07 07:00:0','2017-09-07 12:30:0','I was working!','Working'
GO
EXEC pInsertTime 3,'2017-09-07 12:30:0','2017-09-07 13:00:0','I was eating!','Pause'
GO
EXEC pInsertTime 3,'2017-09-07 13:00:0','2017-09-07 17:30:0','I was working!','Working'
GO
-- 5 
EXEC pInsertTime 3,'2017-09-08 07:00:0','2017-09-08 13:00:0','I was working!','Working'
GO
EXEC pInsertTime 3,'2017-09-08 13:00:0','2017-09-08 13:30:0','I was eating!','Pause'
GO
EXEC pInsertTime 3,'2017-09-08 13:30:0','2017-09-08 17:30:0','I was working!','Working'
GO

SELECT * FROM tblTime
GO
