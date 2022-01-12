CREATE PROCEDURE [dbo].[Pilots_getAllActiv]
	@ColNumToOrder INT
AS
	SELECT * from TablePilots 
	WHERE Disactivated = 0 or Disactivated is NUll
	ORDER BY CASE
				WHEN @ColNumToOrder = 0 THEN CONVERT(VARCHAR(100),LastName) 
				WHEN @ColNumToOrder = 1 THEN CONVERT(VARCHAR(100),FirstName)				
			 END ASC
RETURN 0