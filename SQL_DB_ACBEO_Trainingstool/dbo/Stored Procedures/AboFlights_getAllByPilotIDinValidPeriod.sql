CREATE PROCEDURE [dbo].[AboFlights_getAllByPilotIDinValidPeriod]
	@PilotID int,
	@actYear int
AS
	SELECT * 
	FROM TableAboFlight
	WHERE PilotID = @PilotID
	AND (YEAR(DateBought) = @actYear -1
	OR YEAR(DateBought) = @actYear)
	/*AND (YEAR(DateBought) = YEAR(GETDATE()) -1
	OR YEAR(DateBought) = YEAR(GETDATE()))*/
RETURN 0