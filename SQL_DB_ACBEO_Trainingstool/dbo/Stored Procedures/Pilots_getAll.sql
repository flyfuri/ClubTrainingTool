CREATE PROCEDURE [dbo].[Pilots_getAll]
	@ColNumToOrder INT
AS
	SELECT * from TablePilots 
	ORDER BY CASE
				WHEN @ColNumToOrder = 0 THEN CONVERT(VARCHAR(100),LastName) 
				WHEN @ColNumToOrder = 1 THEN CONVERT(VARCHAR(100),FirstName)				
			 END ASC
RETURN 0