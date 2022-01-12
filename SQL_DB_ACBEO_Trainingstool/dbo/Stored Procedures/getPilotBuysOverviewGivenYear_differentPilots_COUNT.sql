CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_differentPilots_COUNT]
	@YEAR INT
AS
	DECLARE @PILOTS INT

	SET @PILOTS = 0
	
	SELECT @PILOTS = COUNT (DISTINCT TblParticipants.PilotID)
	FROM TblParticipants
	FULL JOIN TableTurns ON TableTurns.ParticipantID = TblParticipants.ParticipantID
	FULL JOIN TableTrainings ON TableTrainings.TrainingID = TableTurns.TrainingID
	AND YEAR(TableTrainings.TrainingDate) = @YEAR
	
RETURN @PILOTS