CREATE PROCEDURE [dbo].[DayPilotCosts_add]
	@TrainingID INT,
	@ParticipantID INT,
	@CostFlights MONEY,
	@CostOtherServices MONEY,
	@CostBuy MONEY,
	@PayedAmount MONEY,
	@PayedFlag MONEY
AS
BEGIN
	INSERT INTO TblDayPilotCosts(TrainingID, ParticipantID, CostFlights, CostOtherServices, CostBuy, PayedAmount, PayedFlag) 
	VALUES (@TrainingID, @ParticipantID, @CostFlights, @CostOtherServices, @CostBuy, @PayedAmount, @PayedFlag)
END