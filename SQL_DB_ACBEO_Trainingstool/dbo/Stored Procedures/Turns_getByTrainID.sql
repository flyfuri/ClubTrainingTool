CREATE PROCEDURE [dbo].[Turns_getByTrainID]
	@TrainingID int
AS
	SELECT * from TableTurns where TrainingID = @TrainingID
RETURN 0