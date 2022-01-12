CREATE PROCEDURE [dbo].[getAbos_byPilotID]
	@PilotID int
AS
	SELECT * 
	FROM TableAbos
	WHERE PilotID = @PilotID
RETURN 0