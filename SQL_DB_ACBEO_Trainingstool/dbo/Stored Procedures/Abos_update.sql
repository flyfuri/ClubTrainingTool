CREATE PROCEDURE [dbo].[Abos_update]
	@Abo_ID INT,
	@PilotID INT,
	@Abo_Nr VARCHAR(50),
	@DateBought DATE,
	@DateFlight1 DATE,
	@DateFlight2 DATE,
	@DateFlight3 DATE,
	@DateFlight4 DATE,
	@DateFlight5 DATE,
	@DateFlight6 DATE,
	@DateFlight7 DATE,
	@DateFlight8 DATE,
	@DateFlight9 DATE,
	@DateFlight10 DATE
AS
BEGIN
	UPDATE TableAbos
	SET		PilotID = @PilotID,
			Abo_Nr = @Abo_Nr,
			DateBought = @DateBought,
			DateFlight1 = @DateFlight1,			
			DateFlight2 = @DateFlight2,
			DateFlight3 = @DateFlight3,
			DateFlight4 = @DateFlight4,
			DateFlight5 = @DateFlight5,
			DateFlight6 = @DateFlight6,
			DateFlight7 = @DateFlight7,
			DateFlight8 = @DateFlight8,
			DateFlight9 = @DateFlight9,
			DateFlight10 = @DateFlight10			
	WHERE   Abo_ID = @Abo_ID
END