CREATE PROCEDURE [dbo].[AboFlights_getAllOfCurrentYear]
	@actYear int
AS
	SELECT * 
	FROM TableAboFlight
	/*WHERE YEAR(DateBought) = YEAR(GETDATE())*/
	WHERE YEAR(DateBought) = @actYear
	ORDER BY Abo_NrInYear DESC
RETURN 0