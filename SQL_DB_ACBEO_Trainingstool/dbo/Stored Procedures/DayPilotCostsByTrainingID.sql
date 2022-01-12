CREATE PROCEDURE [dbo].[DayPilotCostsByTrainingID]
	@TraingingID int
AS
	SELECT *
	FROM TblDayPilotCosts
	WHERE TrainingID = @TraingingID
	ORDER BY DayPilotCostID ASC
RETURN 0