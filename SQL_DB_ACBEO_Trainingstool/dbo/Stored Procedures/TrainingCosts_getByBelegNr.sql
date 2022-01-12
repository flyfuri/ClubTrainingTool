CREATE PROCEDURE [dbo].[TrainingCosts_getByBelegNr]
	@BelegNr TEXT	
AS
	SELECT * 
	FROM TableTrainingCost 
	WHERE CONVERT(VARCHAR(100), BelegNr) = CONVERT(VARCHAR(100), @BelegNr)
	ORDER BY TrainingCostID DESC
RETURN 0