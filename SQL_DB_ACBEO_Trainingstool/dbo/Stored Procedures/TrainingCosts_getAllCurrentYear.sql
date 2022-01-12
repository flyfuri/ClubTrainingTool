CREATE PROCEDURE [dbo].[TrainingCosts_getAllCurrentYear]
AS
	SELECT * from TableTrainingCost 
	WHERE YEAR(TrainingDate) = YEAR(GETDATE());
RETURN 0