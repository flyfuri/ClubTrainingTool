CREATE PROCEDURE [dbo].[getAbo_byAboID]
	@Abo_ID int
AS
	SELECT *
	FROM TableAbos
	WHERE Abo_ID = @Abo_ID
RETURN 0