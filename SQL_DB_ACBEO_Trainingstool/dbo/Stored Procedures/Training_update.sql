CREATE PROCEDURE [dbo].[Training_update]
	@TrainingID INT,
	@TrainingDate DATE,
	@CashAtBegin MONEY,
	@CashToACBEO MONEY,
	@CashAtEnd MONEY,
	@Remarks TEXT,
	@Leiter1_ID INT,
	@Leiter2_ID INT,
	@CashToACBEO_PayedBy TEXT
AS
BEGIN
	UPDATE TableTrainings
	SET		TrainingDate = @TrainingDate,
			CashAtBegin = @CashAtBegin,
			CashToACBEO = @CashToACBEO,
			CashAtEnd = @CashAtEnd,
			Remarks = @Remarks,
			Leiter1_ID = @Leiter1_ID,
			Leiter2_ID = @Leiter2_ID,
			CashToACBEO_payedBy = @CashToACBEO_PayedBy
	WHERE   TrainingID = @TrainingID
END