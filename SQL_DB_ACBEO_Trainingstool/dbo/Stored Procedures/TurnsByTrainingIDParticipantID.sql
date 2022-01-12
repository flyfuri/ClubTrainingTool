CREATE PROCEDURE [dbo].[TurnsByTrainingIDParticipantID]
	@TraingingID int,
	@ParticipantID int
AS
	SELECT TurnID, TrainingID, ParticipantID, TurnNr, Flight
	FROM TableTurns
	WHERE TrainingID = @TraingingID AND ParticipantID = @ParticipantID
	ORDER BY TurnNr ASC
RETURN 0