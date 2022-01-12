CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_FligthsPayedWithoutAbo_COUNT]
	@YEAR INT
AS
	DECLARE @FLUEGE INT,
			@JOBSDONE INT,
			@FLPAYDWABO INT,
			@LEITER1 INT,
			@LEITER2 INT,
			@FLPAYDBAR INT, 
			@UNUSEDABO0FL INT

	SET @FLUEGE = 0
	SET @JOBSDONE = 0
	SET @FLPAYDWABO = 0
	SET @LEITER1 = 0
	SET @LEITER2 = 0
	SET @FLPAYDBAR = 0
	SET @UNUSEDABO0FL = 0

	--number of flights to pay
	SELECT @FLUEGE = COUNT(*)
	FROM TableTurns
	FULL JOIN TableTrainings ON TableTrainings.TrainingID = TableTurns.TrainingID
	WHERE (CONVERT(VARCHAR(MAX), TableTurns.Flight)) = 'FLIGHT'
	AND YEAR(TableTrainings.TrainingDate) = @YEAR

	--number of jobs done
	SELECT @JOBSDONE = COUNT(*)
	FROM TableTurns
	FULL JOIN TableTrainings ON TableTrainings.TrainingID = TableTurns.TrainingID
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR
	AND((CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'BUS')
	    OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'BOAT')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'BandB')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'FnBus')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'FnBoat')
		OR (CONVERT(VARCHAR(MAX), TableTurns.Flight) = 'FnBandB'))

	--number of flights payed with abo
	SELECT @FLPAYDWABO = COUNT(*)
	FROM TableAboFlight
	WHERE YEAR(TableAboFlight.DateFlightPayedWith) = @YEAR

	--number of Leiter jobs (Leiter1)
	SELECT @LEITER1 = COUNT(*)
	FROM TableTrainings
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR
	AND TableTrainings.Leiter1_ID <> 0 
	AND TableTrainings.Leiter1_ID is not NULL

	--number of Leiter jobs (Leiter1)
	SELECT @LEITER2 = COUNT(*)
	FROM TableTrainings
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR
	AND TableTrainings.Leiter2_ID <> 0 
	AND TableTrainings.Leiter2_ID is not NULL

	--number 0Abo-Flights not used
	SELECT @UNUSEDABO0FL = COUNT(*)
	FROM TableAboFlight
	WHERE TableAboFlight.Abo_Year = @YEAR
	AND (TableAboFlight.DateFlightPayedWith < '1800-01-01' ) 
	AND TableAboFlight.Abo_NrInYear = 0

	SET @FLPAYDBAR = @FLUEGE - @FLPAYDWABO - @JOBSDONE - @LEITER1 - @LEITER2 + @UNUSEDABO0FL

RETURN @FLPAYDBAR