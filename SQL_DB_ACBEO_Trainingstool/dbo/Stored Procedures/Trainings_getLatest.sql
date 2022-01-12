CREATE PROCEDURE [dbo].[Trainings_getLatest]
	
AS
	SELECT TOP 1 * from TableTrainings ORDER BY TableTrainings.TrainingDate DESC
RETURN 0