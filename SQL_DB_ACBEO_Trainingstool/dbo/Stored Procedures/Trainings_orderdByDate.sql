CREATE PROCEDURE [dbo].[Trainings_orderdByDate]
	@OrderDESC INT
AS
	SELECT * from TableTrainings 
	ORDER BY
	CASE WHEN @OrderDESC <= 0 THEN TableTrainings.TrainingDate END ASC,
	CASE WHEN @OrderDESC >= 1 THEN TableTrainings.TrainingDate END DESC

RETURN 0