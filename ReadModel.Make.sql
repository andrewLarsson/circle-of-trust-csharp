CREATE TABLE [dbo].[PlayerStats] (
	[PlayerId] UNIQUEIDENTIFIER NOT NULL,
	[Username] VARCHAR(255) NOT NULL,
	[HasCircle] BIT NOT NULL,
	[MemberOfCircles] INT NOT NULL,
	[BetrayedCircles] INT NOT NULL,
	[ClusterId] BIGINT NOT NULL IDENTITY,
	CONSTRAINT [PK_PlayerStats_PlayerId] PRIMARY KEY NONCLUSTERED ([PlayerId])
)
CREATE UNIQUE CLUSTERED INDEX [IX_PlayerStats_ClusterID] ON [dbo].[PlayerStats]([ClusterID])
CREATE INDEX [IX_PlayerStats_Username] ON [dbo].[PlayerStats]([Username])
CREATE INDEX [IX_PlayerStats_HasCircle] ON [dbo].[PlayerStats]([HasCircle])

CREATE TABLE [dbo].[CircleStats] (
	[CircleId] UNIQUEIDENTIFIER NOT NULL,
	[PlayerId] UNIQUEIDENTIFIER NOT NULL,
	[Name] VARCHAR(511) NOT NULL,
	[IsBetrayed] BIT NOT NULL,
	[Members] INT NOT NULL,
	[ClusterId] BIGINT NOT NULL IDENTITY,
	CONSTRAINT [PK_CircleStats_CircleId] PRIMARY KEY NONCLUSTERED ([CircleId])
)
CREATE UNIQUE CLUSTERED INDEX [IX_CircleStats_ClusterID] ON [dbo].[CircleStats]([ClusterID])
CREATE INDEX [IX_CircleStats_PlayerId] ON [dbo].[CircleStats]([PlayerId])
CREATE INDEX [IX_CircleStats_Name] ON [dbo].[CircleStats]([Name])
CREATE INDEX [IX_CircleStats_IsBetrayed] ON [dbo].[CircleStats]([IsBetrayed])

CREATE TABLE [dbo].[CircleLeaderboardContender] (
	[CircleLeaderboardContenderId] INT NOT NULL IDENTITY,
	[CircleId] UNIQUEIDENTIFIER NOT NULL,
	[Members] INT NOT NULL,
	CONSTRAINT [PK_CircleLeaderboardContender_CircleLeaderboardContenderId] PRIMARY KEY ([CircleLeaderboardContenderId])
)
CREATE INDEX [IX_CircleLeaderboardContender_CircleId] ON [dbo].[CircleLeaderboardContender]([CircleId])
CREATE INDEX [IX_CircleLeaderboardContender_Members] ON [dbo].[CircleLeaderboardContender]([Members])
