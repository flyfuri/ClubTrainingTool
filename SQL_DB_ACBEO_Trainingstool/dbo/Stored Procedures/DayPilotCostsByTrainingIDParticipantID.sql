CREATE PROCEDURE [dbo].[DayPilotCostsByTrainingIDParticipantID]
	@TraingingID int,
	@ParticipantID int
AS
	SELECT *
	FROM TblDayPilotCosts
	WHERE TrainingID = @TraingingID AND ParticipantID = @ParticipantID
	ORDER BY DayPilotCostID ASC
RETURN 0