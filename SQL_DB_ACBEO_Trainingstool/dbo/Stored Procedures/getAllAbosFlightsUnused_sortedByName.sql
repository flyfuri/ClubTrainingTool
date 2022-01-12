CREATE PROCEDURE [dbo].[getAllAbosFlightsUnused_sortedByName]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TableAboFlight.Abo_NrInYear, TablePilots.FirstName, TablePilots.LastName, TableAboFlight.DateFlightPayedWith
	FROM TableTrainings
	FULL JOIN TableAboFlight ON TableAboFlight.DateBought = TableTrainings.TrainingDate 
	FULL JOIN TablePilots on TablePilots.Pilot_ID = TableAboFlight.PilotID
	WHERE (YEAR(TableTrainings.TrainingDate) = @YEAR OR YEAR(TableTrainings.TrainingDate) = @YEAR -1)
	AND (TableAboFlight.DateFlightPayedWith < '1800-01-01' )
	
	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0