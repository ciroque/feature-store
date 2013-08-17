
/* *********************************************************************************************************************
 *	Version 1.0.0.0 Script for the Feature Store database.
 *	
 *
 ******************************************************************************************************************** */


/* @@ 

	Schema Definition 

@@ */

if not exists (select * from sys.schemas where [name] = 'FeatureStore')
	exec('create schema FeatureStore')
go

/* @@
	
	Previous object cleanup  (obviously intended for scratch-builds only!)
		
@@ */

IF OBJECT_ID('FeatureStore.Feature', 'U') IS NOT NULL
	DROP TABLE FeatureStore.Feature

IF OBJECT_ID('FeatureStore.FeatureUniqueness', 'U') IS NOT NULL
	DROP TABLE FeatureStore.FeatureUniqueness

IF OBJECT_ID('FeatureStore.Version', 'U') IS NOT NULL
	DROP TABLE FeatureStore.[Version]

IF OBJECT_ID('FeatureStore.Log', 'U') IS NOT NULL
	DROP TABLE FeatureStore.[Log]

IF OBJECT_ID('FeatureStore.Retrieve' ,'P') IS NOT NULL
	DROP PROC FeatureStore.Retrieve
GO

IF OBJECT_ID('FeatureStore.Store' ,'P') IS NOT NULL
	DROP PROC FeatureStore.Store
GO

IF OBJECT_ID('FeatureStore.WriteLogEntry' ,'P') IS NOT NULL
	DROP PROC FeatureStore.WriteLogEntry
GO

IF OBJECT_ID('FeatureStore.RetrieveOne' ,'P') IS NOT NULL
	DROP PROC FeatureStore.RetrieveOne
GO

IF OBJECT_ID('FeatureStore.IsHealthy_ObjectExistenceCheck', 'P') IS NOT NULL
	DROP PROC FeatureStore.IsHealthy_ObjectExistenceCheck
GO

IF DATABASE_PRINCIPAL_ID('FeatureStore_Writer') IS NOT NULL
	DROP ROLE FeatureStore_Writer

IF DATABASE_PRINCIPAL_ID('FeatureStore_Reader') IS NOT NULL
	DROP ROLE FeatureStore_Reader

/* @@
	
	Roles
		
@@ */

CREATE ROLE FeatureStore_Reader
CREATE ROLE FeatureStore_Writer

exec sp_addrolemember @rolename = 'FeatureStore_Reader', @membername = 'FeatureStore_Writer'

/* @@
	
	Tables and indexes
		
@@ */

CREATE TABLE FeatureStore.FeatureUniqueness
(
	FeatureUniquenessId INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	, UniqueId UNIQUEIDENTIFIER NOT NULL
)

CREATE TABLE FeatureStore.Feature
(
	Id INT NOT NULL 
	, OwnerId INT NOT NULL CONSTRAINT FK_FeatureOwnerId_FeatureUniqueness FOREIGN KEY REFERENCES FeatureStore.FeatureUniqueness(FeatureUniquenessId)
	, SpaceId INT NOT NULL CONSTRAINT FK_FeatureSpaceId_FeatureUniqueness FOREIGN KEY REFERENCES FeatureStore.FeatureUniqueness(FeatureUniquenessId)
	, Name SYSNAME NOT NULL
	, [Enabled] BIT NOT NULL CONSTRAINT DF_Enabled DEFAULT(0)
	, CONSTRAINT PK_Feature PRIMARY KEY (Id, OwnerId, SpaceId)
)

CREATE INDEX IDX_FeatureUniqueness_UniqueId ON FeatureStore.FeatureUniqueness(UniqueId)

CREATE TABLE FeatureStore.[Version]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[VersionString] NVARCHAR(64) NOT NULL,
	[ScriptName] SYSNAME NOT NULL,
	[DateApplied] DATETIME NOT NULL CONSTRAINT DF_DateApplied DEFAULT GETDATE()
)

CREATE TABLE [FeatureStore].[Log] 
( 
  [ID] [int] IDENTITY (1, 1) NOT NULL ,
  [Date] DATETIME NOT NULL ,
  [Thread] NVARCHAR (255) NOT NULL ,
  [Level] NVARCHAR (20) NOT NULL ,
  [Logger] NVARCHAR (255) NOT NULL ,
  [Message] NVARCHAR (4000) NOT NULL,
  [Exception] NVARCHAR(2000) NULL
) ON [PRIMARY]

GO

/* @@
	
	Stored Procedures
		
@@ */


CREATE PROC FeatureStore.Store
(
	@Id INT,
	@OwnerUid UNIQUEIDENTIFIER,
	@SpaceUid UNIQUEIDENTIFIER,
	@Name SYSNAME,
	@Enabled BIT = 0
)	
AS

	IF (EXISTS (SELECT 1 
		FROM Feature f
		JOIN FeatureUniqueness funOwner
			ON f.OwnerId = funOwner.FeatureUniquenessId
		JOIN FeatureUniqueness funSpace
			ON f.SpaceId = funSpace.FeatureUniquenessId
		WHERE f.Id = @Id 
		AND funOwner.UniqueId = @OwnerUid
		AND funSpace.UniqueId = @SpaceUid))
	 BEGIN

		/*** Updating Existing ***/

		UPDATE f
		SET f.[Enabled] = @Enabled
		FROM Feature f
		JOIN FeatureUniqueness funOwner
			ON f.OwnerId = funOwner.FeatureUniquenessId
		JOIN  FeatureUniqueness funSpace
			ON f.SpaceId = funSpace.FeatureUniquenessId
		WHERE f.Id = @Id 
		AND funOwner.UniqueId = @OwnerUid
		AND funSpace.UniqueId = @SpaceUid
	 END

	ELSE
	 BEGIN
	 
		/*** Insert New Feature ***/

		/*** Ensure Owner Unique Identifier ***/
		DECLARE @OwnerUniqueId INT
		SET @OwnerUniqueId = (SELECT FeatureUniquenessId FROM FeatureUniqueness WHERE UniqueId = @OwnerUid)
		IF @OwnerUniqueId IS NULL
		 BEGIN
			INSERT INTO FeatureUniqueness(UniqueId) VALUES(@OwnerUid)
			SELECT @OwnerUniqueId = SCOPE_IDENTITY()
		 END
		 
		/*** Ensure Space Unique Identifier ***/
		DECLARE @SpaceUniqueId INT
		SET @SpaceUniqueId = (SELECT FeatureUniquenessId FROM FeatureUniqueness WHERE UniqueId = @SpaceUid)
		IF @SpaceUniqueId IS NULL
		 BEGIN
			INSERT INTO FeatureUniqueness(UniqueId) VALUES(@SpaceUid)
			SELECT @SpaceUniqueId = SCOPE_IDENTITY()
		 END
		 
		 /*** Actually insert the Feature ***/
		INSERT INTO Feature([Id], [OwnerId], [SpaceId], [Name], [Enabled])
		VALUES (@Id, @OwnerUniqueId, @SpaceUniqueId, @Name, @Enabled)
		
	END
GO

CREATE PROC FeatureStore.RetrieveOne
(
	@Id INT,
	@OwnerUid UNIQUEIDENTIFIER,
	@SpaceUid UNIQUEIDENTIFIER
)	
AS

	SELECT 
		f.[Id],
		funOwner.[UniqueId] AS OwnerUid,
		funSpace.[UniqueId] AS SpaceUid,
		f.[Name],
		f.[Enabled]
		FROM Feature f
		JOIN FeatureUniqueness funOwner
			ON f.OwnerId = funOwner.FeatureUniquenessId
		JOIN FeatureUniqueness funSpace
			ON f.SpaceId = funSpace.FeatureUniquenessId
		WHERE f.Id = @Id 
		AND funOwner.UniqueId = @OwnerUid
		AND funSpace.UniqueId = @SpaceUid
GO

CREATE PROC FeatureStore.Retrieve
(
	@OwnerUid UNIQUEIDENTIFIER = NULL,
	@SpaceUid UNIQUEIDENTIFIER = NULL
)	
AS

	SELECT 
		f.[Id],
		funOwner.[UniqueId] AS OwnerUid,
		funSpace.[UniqueId] AS SpaceUid,
		f.[Name],
		f.[Enabled]
		FROM Feature f
		JOIN FeatureUniqueness funOwner
			ON f.OwnerId = funOwner.FeatureUniquenessId
		JOIN FeatureUniqueness funSpace
			ON f.SpaceId = funSpace.FeatureUniquenessId
		WHERE 
			(@OwnerUid IS NULL OR funOwner.UniqueId = @OwnerUid)
		AND (@SpaceUid IS NULL OR funSpace.UniqueId = @SpaceUid)
GO

CREATE PROC FeatureStore.WriteLogEntry
(
	@Date DATETIME,
	@Thread NVARCHAR(255),
	@Level NVARCHAR(20),
	@Logger NVARCHAR(255),
	@Message NVARCHAR(4000),
	@Exception NVARCHAR(2000)
)
AS

	INSERT INTO [FeatureStore].[Log]
	(
		[Date],
		[Thread],
		[Level],
		[Logger],
		[Message],
		[Exception]
	)
	VALUES
	(
		@Date,
		@Thread,
		@Level,
		@Logger, 
		@Message,
		@Exception
	) 
GO
	
CREATE PROC FeatureStore.IsHealthy_ObjectExistenceCheck
AS

SELECT 
	CASE WHEN DATABASE_PRINCIPAL_ID('FeatureStore_Reader') IS NULL THEN 0 ELSE 1 END AS ReaderRole
	, CASE WHEN DATABASE_PRINCIPAL_ID('FeatureStore_Writer') IS NULL THEN 0 ELSE 1 END AS WriterRole
	, CASE WHEN OBJECT_ID('FeatureStore.FeatureUniqueness', 'U') IS NULL THEN 0 ELSE 1 END AS FeatureUniquenessTable
	, CASE WHEN OBJECT_ID('FeatureStore.Feature', 'U') IS NULL THEN 0 ELSE 1 END AS FeatureTable
	, CASE WHEN OBJECT_ID('FeatureStore.Version', 'U') IS NULL THEN 0 ELSE 1 END AS VersionTable
	, CASE WHEN OBJECT_ID('FeatureStore.Log', 'U') IS NULL THEN 0 ELSE 1 END AS LogTable
	, CASE WHEN OBJECT_ID('FeatureStore.Store', 'P') IS NULL THEN 0 ELSE 1 END AS StoreSproc
	, CASE WHEN OBJECT_ID('FeatureStore.RetrieveOne', 'P') IS NULL THEN 0 ELSE 1 END AS RetrieveOneSproc
	, CASE WHEN OBJECT_ID('FeatureStore.Retrieve', 'P') IS NULL THEN 0 ELSE 1 END AS RetrieveSproc
	, CASE WHEN OBJECT_ID('FeatureStore.WriteLogEntry', 'P') IS NULL THEN 0 ELSE 1 END AS WriteLogEntrySproc

GO

/* @@
[Message]	
	Permissions
		
@@ */

GRANT SELECT ON FeatureStore.FeatureUniqueness TO FeatureStore_Reader
GRANT SELECT ON FeatureStore.Feature TO FeatureStore_Reader

GRANT EXEC ON FeatureStore.Store TO FeatureStore_Writer
GRANT EXEC ON FeatureStore.RetrieveOne TO FeatureStore_Reader
GRANT EXEC ON FeatureStore.Retrieve TO FeatureStore_Reader

GRANT EXEC ON FeatureStore.WriteLogEntry TO FeatureStore_Writer

GO

/* @@
	
	Update Version
		
@@ */

INSERT INTO FeatureStore.Version([VersionString], [ScriptName], [DateApplied])
VALUES('1.0.0.0', 'FeatureStore-1.0.0.0.sql', GETDATE())

SELECT * FROM [FeatureStore].[Version]