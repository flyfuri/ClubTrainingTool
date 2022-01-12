CREATE PROCEDURE [dbo].[AboFlight_getUsableOfPilotBoughtThisTraining]
	@PilotID INT,
	@TrainingID INT,
	@Abo_NrInYEAR INT
AS
	SELECT TableAboFlight.*
	FROM TableAboFlight 
	INNER JOIN TableTrainings ON TableAboFlight.DateBought = TableTrainings.TrainingDate
	WHERE TableAboFlight.PilotID = @PilotID 
	AND TableTrainings.TrainingID = @TrainingID
	AND TableAboFlight.DateFlightPayedWith < '1800-01-01'	
	AND TableAboFlight.Abo_NrInYear = @Abo_NrInYEAR
	ORDER BY TableAboFlight.Abo_NrInYear DESC
RETURN 0