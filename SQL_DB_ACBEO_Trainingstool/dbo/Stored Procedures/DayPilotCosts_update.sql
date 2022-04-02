CREATE PROCEDURE [dbo].[DayPilotCosts_update]
	@DayPilotCostsID INT,
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
	UPDATE TblDayPilotCosts 	
	SET		TrainingID = @TrainingID, 
			ParticipantID = @ParticipantID,
			CostFlights = @CostFlights,
			CostOtherServices = @CostOtherServices,
			CostBuy = @CostBuy,
			PayedAmount = @PayedAmount,
			PayedFlag = @PayedFlag,
			PayedTwint = @PayedTwint, 
			PayedTwintReference = @PayedTwintReference
	WHERE   DayPilotCostID = @DayPilotCostsID
END