DELETE FROM [CircleOfTrust].[dbo].[BetrayedCircle];
DELETE FROM [CircleOfTrust].[dbo].[Member];
DELETE FROM [CircleOfTrust].[dbo].[Circle];
DELETE FROM [CircleOfTrust].[dbo].[Player];

DELETE FROM [CircleOfTrustView].[dbo].[CircleLeaderboardContender];
DELETE FROM [CircleOfTrustView].[dbo].[CircleStats];
DELETE FROM [CircleOfTrustView].[dbo].[PlayerStats];
INSERT INTO [CircleOfTrustView].[dbo].[CircleLeaderboardContender]
	(CircleId, Members)
VALUES
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0)
;
