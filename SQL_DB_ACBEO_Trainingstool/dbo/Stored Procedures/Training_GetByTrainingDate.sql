CREATE PROCEDURE [dbo].[Training_GetByTrainingDate]
	@TrainingDate Date
AS
	SELECT * from TableTrainings WHERE TrainingDate = @TrainingDate
RETURN 0