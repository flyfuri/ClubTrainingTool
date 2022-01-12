CREATE PROCEDURE [dbo].[Training_GetByYEAR]
	@YEAR int
AS
	SELECT * from TableTrainings WHERE YEAR(TrainingDate) = @YEAR
RETURN 0