CREATE PROCEDURE [dbo].[Trainings_getOverviewCurrentYear]
AS
	SELECT TrainingDate, CashAtBegin, CashToACBEO, CashAtEnd, Remarks 
	from TableTrainings 
	WHERE YEAR(TrainingDate) = YEAR(GETDATE());
RETURN 0