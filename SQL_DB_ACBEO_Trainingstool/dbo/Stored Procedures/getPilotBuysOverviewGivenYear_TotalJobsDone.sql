CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_TotalJobsDone]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TableTurns.Flight
	FROM TableTurns
	FULL JOIN TableTrainings ON TableTrainings.TrainingID = TableTurns.TrainingID
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR
	AND((CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'BUS')
	    OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'BOAT')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'BandB')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'FnBus')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'FnBoat')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'FnBandB'))

	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0