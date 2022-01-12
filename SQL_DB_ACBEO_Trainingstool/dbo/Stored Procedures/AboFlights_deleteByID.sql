CREATE PROCEDURE [dbo].[AboFlights_deleteByID]
	@AboFlight_ID int
AS
	DELETE FROM TableAboFlight 
	WHERE AboFlight_ID = @AboFlight_ID
RETURN 0