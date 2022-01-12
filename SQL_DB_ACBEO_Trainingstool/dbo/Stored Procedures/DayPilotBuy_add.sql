CREATE PROCEDURE [dbo].[DayPilotBuy_add]
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
	INSERT INTO TableDayPilotBuy (TrainingID, ParticipantID, LifewestBuy, LifewestRent, YearFee, DayFee, AxalpWeekFee, Others, Remarks ) 
	VALUES (@TrainingID, @ParticipantID, @LifewestBuy, @LifewestRent, @YearFee, @DayFee, @AxalpWeekFee, @Others, @Remarks)
END