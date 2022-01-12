CREATE PROCEDURE [dbo].[AboFlight_getAboFlightsPayedBackWithDayPilotID]
	@PilotID INT,
	@TrainingID INT
AS
	SELECT TableAboFlight.*
	FROM TableAboFlight 
	INNER JOIN TableTrainings ON TableAboFlight.DateBought = TableTrainings.TrainingDate
	WHERE TableAboFlight.PilotID = @PilotID 
	AND TableTrainings.TrainingID = @TrainingID	
	ORDER BY AboFlight_ID ASC
RETURN 0