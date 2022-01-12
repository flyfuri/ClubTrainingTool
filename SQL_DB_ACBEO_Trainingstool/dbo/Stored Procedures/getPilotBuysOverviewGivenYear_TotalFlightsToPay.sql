CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_TotalFlightsToPay]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TableTurns.Flight
	FROM TableTurns
	FULL JOIN TableTrainings ON TableTrainings.TrainingID = TableTurns.TrainingID
	WHERE (CONVERT(VARCHAR(MAX), TableTurns.Flight)) = 'FLIGHT'
	AND YEAR(TableTrainings.TrainingDate) = @YEAR

	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0