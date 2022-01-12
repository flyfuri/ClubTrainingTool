CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_AbosFlightsUnused]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TableAboFlight.Abo_NrInYear, TablePilots.FirstName, TablePilots.LastName
	FROM TableTrainings
	FULL JOIN TableAboFlight ON TableAboFlight.DateBought = TableTrainings.TrainingDate 
	FULL JOIN TablePilots on TablePilots.Pilot_ID = TableAboFlight.PilotID
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR 
	AND (TableAboFlight.DateFlightPayedWith < '1800-01-01' )
	
	ORDER BY TableAboFlight.Abo_NrInYear ASC
RETURN 0