CREATE PROCEDURE [dbo].[Trainings_add]
	@TrainingDate DateTime,
	@CashAtBegin MONEY,
	@CashToACBEO MONEY,
	@CashAtEnd MONEY,
	@Remarks  TEXT,
    @Leiter1_ID INT,
    @Leiter2_ID INT,
	@CashToACBEO_PayedBy TEXT,
	@PayedTwintReference TEXT,
	@Finalized	BIT
AS
BEGIN
	INSERT INTO TableTrainings(TrainingDate, CashAtBegin, CashToACBEO, CashAtEnd, Remarks, Leiter1_ID, Leiter2_ID, CashToACBEO_payedBy, PayedTwintReference, Finalized) 
	VALUES (@TrainingDate, @CashAtBegin, @CashToACBEO, @CashAtEnd, @Remarks, @Leiter1_ID, @Leiter2_ID, @CashToACBEO_PayedBy, @PayedTwintReference, @Finalized)
END