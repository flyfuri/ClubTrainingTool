CREATE PROCEDURE [dbo].[Turn_getByTurnID]
	@TurnID int
AS
	SELECT * from TableTurns WHERE TurnID = @TurnID;
RETURN 0