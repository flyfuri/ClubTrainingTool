CREATE PROCEDURE [dbo].[DayPilotBuy_update]
	@DayPilotBuyID INT,
	@TrainingID INT,
	@ParticipantID INT,
	@LifewestBuy MONEY,
	@LifewestRent MONEY,
	@YearFee MONEY,
	@DayFee MONEY,
	@AxalpWeekFee MONEY,
	@Others MONEY,
	@Remarks TEXT
AS
BEGIN
	UPDATE TableDayPilotBuy
	SET		TrainingID = @TrainingID, 
			ParticipantID = @ParticipantID,
			LifewestBuy = @LifewestBuy,
			LifewestRent = @LifewestRent,
			YearFee = @YearFee,
			DayFee = @DayFee,
			AxalpWeekFee = @AxalpWeekFee,
			Others = @Others,
			Remarks = @Remarks
	WHERE   DayPilotBuyID = @DayPilotBuyID
END