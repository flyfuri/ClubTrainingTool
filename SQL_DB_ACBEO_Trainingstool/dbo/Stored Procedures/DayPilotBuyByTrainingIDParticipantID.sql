CREATE PROCEDURE [dbo].[DayPilotBuyByTrainingIDParticipantID]
	@TraingingID int,
	@ParticipantID int
AS
	SELECT *
	FROM TableDayPilotBuy
	WHERE TrainingID = @TraingingID AND ParticipantID = @ParticipantID
	ORDER BY DayPilotBuyID ASC
RETURN 0