CREATE PROCEDURE [dbo].[DayPilotCosts_add]
	@TrainingID INT,
	@ParticipantID INT,
	@CostFlights MONEY,
	@CostOtherServices MONEY,
	@CostBuy MONEY,
	@PayedAmount MONEY,
	@PayedFlag BIT,
	@PayedTwint MONEY,
	@PayedTwintReference TEXT
AS
BEGIN
	INSERT INTO TblDayPilotCosts(TrainingID, ParticipantID, CostFlights, CostOtherServices, CostBuy, PayedAmount, PayedFlag, PayedTwint, PayedTwintReference) 
	VALUES (@TrainingID, @ParticipantID, @CostFlights, @CostOtherServices, @CostBuy, @PayedAmount, @PayedFlag, @PayedTwint, @PayedTwintReference)
END