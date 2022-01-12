CREATE PROCEDURE [dbo].[AboFlights_getAllUsableByPilotID]
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
	AND DateFlightPayedWith < '1800-01-01'
	
RETURN 0