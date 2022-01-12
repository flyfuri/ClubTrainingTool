CREATE PROCEDURE [dbo].[DayPilotCosts_update]
	@DayPilotCostsID INT,
	@TrainingID INT,
	@ParticipantID INT,
	@CostFlights MONEY,
	@CostOtherServices MONEY,
	@CostBuy MONEY,
	@PayedAmount MONEY,
	@PayedFlag MONEY
AS
BEGIN
	UPDATE TblDayPilotCosts 	
	SET		TrainingID = @TrainingID, 
			ParticipantID = @ParticipantID,
			CostFlights = @CostFlights,
			CostOtherServices = @CostOtherServices,
			CostBuy = @CostBuy,
			PayedAmount = @PayedAmount,
			PayedFlag = @PayedFlag

	WHERE   DayPilotCostID = @DayPilotCostsID
END