CREATE PROCEDURE [dbo].[AboFlights_updateDateFlightPayedWith]
	@AboFlight_ID INT,
	@DateFlightPayedWith DATE
AS
BEGIN
	UPDATE TableAboFlight
	SET		DateFlightPayedWith = @DateFlightPayedWith		
	WHERE   AboFlight_ID = @AboFlight_ID
END