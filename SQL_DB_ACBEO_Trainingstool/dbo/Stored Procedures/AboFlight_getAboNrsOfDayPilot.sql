CREATE PROCEDURE [dbo].[AboFlight_getAboNrsOfDayPilot]
	@PilotID INT,
	@TrainingID INT
AS
	SELECT TableAboFlight.*
	FROM TableAboFlight 
	INNER JOIN TableTrainings ON TableAboFlight.DateBought = TableTrainings.TrainingDate
	WHERE TableAboFlight.PilotID = @PilotID 
	AND TableTrainings.TrainingID = @TrainingID
	AND FlightNrOnAbo = 1	
	ORDER BY TableAboFlight.Abo_NrInYear DESC
RETURN 0