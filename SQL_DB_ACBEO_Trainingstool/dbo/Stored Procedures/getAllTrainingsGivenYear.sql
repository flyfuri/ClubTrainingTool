CREATE PROCEDURE [dbo].[getAllTrainingsGivenYear]
	@actYear int
AS
	SELECT * 
	FROM TableTrainings
	WHERE YEAR(TrainingDate) = @actYear
	ORDER BY TrainingDate DESC
RETURN 0