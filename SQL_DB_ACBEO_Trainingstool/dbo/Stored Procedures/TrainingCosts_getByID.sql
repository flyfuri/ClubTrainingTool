CREATE PROCEDURE [dbo].[TrainingCosts_getByID]
	@TrainingCostID int
AS
	SELECT * from TableTrainingCost where TrainingCostID = @TrainingCostID;
RETURN 0