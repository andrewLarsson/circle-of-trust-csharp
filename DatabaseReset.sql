DELETE FROM [CircleOfTrust].[dbo].[CircleLeaderboardContender];
DELETE FROM [CircleOfTrust].[dbo].[CircleStats];
DELETE FROM [CircleOfTrust].[dbo].[PlayerStats];
DELETE FROM [CircleOfTrust].[dbo].[BetrayedCircle];
DELETE FROM [CircleOfTrust].[dbo].[Member];
DELETE FROM [CircleOfTrust].[dbo].[Circle];
DELETE FROM [CircleOfTrust].[dbo].[Player];

INSERT INTO [CircleOfTrust].[dbo].[CircleLeaderboardContender]
	(CircleId, Members)
VALUES
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0),
	('00000000-0000-0000-0000-000000000000', 0)
;
