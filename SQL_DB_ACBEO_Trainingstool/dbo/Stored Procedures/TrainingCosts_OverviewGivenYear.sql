CREATE PROCEDURE [dbo].[TrainingCosts_OverviewGivenYear]
	@YEAR INT           
AS
	SELECT * from TableTrainingCost 
	WHERE YEAR(TrainingDate) = @YEAR
RETURN 0