PRINT N'Creating Tables';
PRINT N'-------------------';
PRINT N'';

PRINT N'Creating Database...';

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'CarReservation')
BEGIN
	USE [master]

	-- Kill all Connections to Database
	DECLARE @Spid 					INT,
			@SpidCurrentConnection	INT,
			@Sql 					NVARCHAR(MAX),
			@DatabaseName			NVARCHAR(MAX)

	SET @DatabaseName			= DB_NAME()
	SET @SpidCurrentConnection	= @@SPID

	DECLARE CurrentProcess CURSOR FOR 
		SELECT 		p.SPID 
		FROM 		sys.databases		db 
		INNER JOIN 	sys.sysprocesses 	p	ON db.database_id = p.dbid
		WHERE 		db.name = @DatabaseName 
			AND 	p.SPID > 50 
			AND		p.SPID <> @SpidCurrentConnection

	OPEN CurrentProcess FETCH NEXT FROM CurrentProcess INTO @Spid 
	WHILE @@FETCH_STATUS = 0 
	BEGIN 
		SET @Sql = 'KILL ' + CONVERT(NVARCHAR(30), @Spid) 
		PRINT @Sql 
		EXECUTE(@Sql) 
	FETCH NEXT FROM CurrentProcess INTO @Spid END CLOSE CurrentProcess DEALLOCATE CurrentProcess

	-- Drop Database
	DROP DATABASE CarReservation
END

CREATE DATABASE CarReservation

GO

USE [CarReservation]


PRINT N'Creating dbo.reservations...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'reservations')
DROP TABLE [dbo].[reservations]
CREATE TABLE [dbo].[reservations] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [CarId]     INT      NOT NULL,
    [CustomerId]    INT      NOT NULL,
    [From]        DATETIME NOT NULL,
    [To]        DATETIME NOT NULL,
	[RowVersion] TIMESTAMP
);


PRINT N'Creating dbo.cars...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'cars')
DROP TABLE [dbo].[cars]
CREATE TABLE [dbo].[cars] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Brand]      NVARCHAR (20) NOT NULL,
    [CarClass] INT           NOT NULL,
    [DailyRate] INT           NOT NULL,
    [BaseRate] INT           NULL,
	[RowVersion] TIMESTAMP
);


PRINT N'Creating dbo.customers...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'customers')
DROP TABLE [dbo].[customers]
CREATE TABLE [dbo].[customers] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Lastname]     NVARCHAR (20) NOT NULL,
    [Firstname]      NVARCHAR (20) NOT NULL,
    [Birthday] DATETIME      NOT NULL,
	[RowVersion]   TIMESTAMP
) ON [PRIMARY];


PRINT N'Creating dbo.PK_Car...';

ALTER TABLE [dbo].[cars]
    ADD CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.PK_Customer...';

ALTER TABLE [dbo].[customers]
    ADD CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.PK_Reservation...';

ALTER TABLE [dbo].[reservations]
    ADD CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.FK_Reservation_Car...';

ALTER TABLE [dbo].[reservations]
    ADD CONSTRAINT [FK_Reservation_Car] FOREIGN KEY ([CarId]) REFERENCES [dbo].[cars] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

PRINT N'Creating dbo.FK_Reservation_Customer...';

ALTER TABLE [dbo].[reservations]
    ADD CONSTRAINT [FK_Reservation_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[customers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;
	
PRINT N'Generating Test Data'

SET IDENTITY_INSERT cars ON
INSERT INTO cars (Id, Brand, CarClass, DailyRate, BaseRate)
    SELECT 1, 'Fiat Punto', 2, 50, 0 UNION
    SELECT 2, 'VW Golf', 1, 120, 0 UNION
    SELECT 3, 'Audi S6', 0, 180, 50
SET IDENTITY_INSERT cars OFF

SET IDENTITY_INSERT customers ON
INSERT INTO customers (Id, Lastname, Firstname, Birthday)
    SELECT 1, 'Nass', 'Anna', '1961-05-05 00:00:00' UNION
    SELECT 2, 'Beil', 'Timo', '1980-09-09 00:00:00' UNION
    SELECT 3, 'Pfahl', 'Martha', '1950-07-03 00:00:00' UNION
    SELECT 4, 'Zufall', 'Rainer', '1944-11-11 00:00:00'
SET IDENTITY_INSERT customers OFF
	
SET IDENTITY_INSERT reservations ON
INSERT INTO reservations (Id, CarId, CustomerId, "From", "To")				
   SELECT 1, 1, 1, '2020-01-10 00:00:00', '2020-01-20 00:00:00' UNION				
   SELECT 2, 2, 2, '2020-01-10 00:00:00', '2020-01-20 00:00:00' UNION				
   SELECT 3, 3, 3, '2020-01-10 00:00:00', '2020-01-20 00:00:00'				
SET IDENTITY_INSERT reservations OFF
				
PRINT N'';
PRINT N'-------------------';
PRINT N'Script end...';