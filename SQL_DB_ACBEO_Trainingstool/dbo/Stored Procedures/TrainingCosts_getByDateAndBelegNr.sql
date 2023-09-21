﻿CREATE PROCEDURE [dbo].[TrainingCosts_getByDateAndBelegNr]
	@TrainingDate DATE,
	@BelegNr TEXT	
AS
	SELECT * 
	FROM TableTrainingCost 
	WHERE CONVERT(VARCHAR(100), BelegNr) = CONVERT(VARCHAR(100), @BelegNr)
	and TrainingDate = @TrainingDate
	ORDER BY TrainingCostID DESC
RETURN 0