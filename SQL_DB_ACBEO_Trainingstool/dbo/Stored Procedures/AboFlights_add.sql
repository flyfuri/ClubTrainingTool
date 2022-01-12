CREATE PROCEDURE [dbo].[AboFlights_add]
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
	INSERT INTO TableAboFlight(PilotID, Abo_Year, Abo_NrInYear, FlightNrOnAbo, DateBought, DateFlightPayedWith, Comment, SellerID) 
	VALUES (@PilotID, @Abo_YEAR, @Abo_NrInYEAR, @FlightNrOnAbo, @DateBought, @DateFlightPayedWith, @Comment, @SellerID)
END