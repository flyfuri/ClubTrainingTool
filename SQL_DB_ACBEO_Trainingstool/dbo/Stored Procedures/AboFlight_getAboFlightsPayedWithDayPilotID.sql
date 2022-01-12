CREATE PROCEDURE [dbo].[AboFlight_getAboFlightsPayedWithDayPilotID]
	@PilotID INT,
	@TrainingID INT
AS
	SELECT TableAboFlight.*
	FROM TableAboFlight 
	INNER JOIN TableTrainings ON TableAboFlight.DateFlightPayedWith = TableTrainings.TrainingDate
	WHERE TableAboFlight.PilotID = @PilotID 
	AND TableTrainings.TrainingID = @TrainingID	
	ORDER BY AboFlight_ID ASC
RETURN 0