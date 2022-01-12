CREATE PROCEDURE [dbo].[TrainingCosts_getByDate]
	@TrainingDate DATE
AS
	SELECT * from TableTrainingCost where TrainingDate = @TrainingDate;
RETURN 0