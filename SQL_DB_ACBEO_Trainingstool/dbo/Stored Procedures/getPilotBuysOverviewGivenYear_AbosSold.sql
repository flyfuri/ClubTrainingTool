CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_AbosSold]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TableAboFlight.Abo_NrInYear, TablePilots.FirstName, TablePilots.LastName
	FROM TableTrainings
	FULL JOIN TableAboFlight ON TableAboFlight.DateBought = TableTrainings.TrainingDate 
	FULL JOIN TablePilots on TablePilots.Pilot_ID = TableAboFlight.PilotID
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR 
	AND (TableAboFlight.FlightNrOnAbo = 1)
	and (TableAboFlight.Abo_NrInYear > 0)
	
	ORDER BY TableAboFlight.Abo_NrInYear ASC
RETURN 0