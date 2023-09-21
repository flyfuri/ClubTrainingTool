﻿CREATE PROCEDURE [dbo].[getAllAbosCurrentYear]
AS
	SELECT * 
	FROM TableAbos
	WHERE YEAR(DateBought) = YEAR(GETDATE())
	ORDER BY Abo_Nr DESC
RETURN 0