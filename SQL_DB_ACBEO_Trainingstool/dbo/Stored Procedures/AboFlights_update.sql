CREATE PROCEDURE [dbo].[AboFlights_update]
	@AboFlight_ID INT,
	@PilotID INT,
	@Abo_YEAR INT,
	@Abo_NrInYEAR INT,
	@FlightNrOnAbo INT,
	@DateBought DATE,
	@DateFlightPayedWith DATE,
	@Comment TEXT,
	@SellerID INT
AS
BEGIN
	UPDATE TableAboFlight
	SET		PilotID = @PilotID,
			Abo_Year = @Abo_YEAR,
			Abo_NrInYear = @Abo_NrInYEAR,
			FlightNrOnAbo = @FlightNrOnAbo,
			DateBought = @DateBought,
			DateFlightPayedWith = @DateFlightPayedWith,	
			Comment = @Comment,	
			SellerID = @SellerID
	WHERE   AboFlight_ID = @AboFlight_ID
END