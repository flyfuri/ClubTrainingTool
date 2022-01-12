CREATE PROCEDURE [dbo].[Training_GetByID]
	@TrainingID int
AS
	SELECT * from TableTrainings WHERE TrainingID = @TrainingID
RETURN 0