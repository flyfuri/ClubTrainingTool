CREATE PROCEDURE [dbo].[TrainingCosts_update]
	@TrainingCostID INT,
	@TrainingDate DATE,
	@Betrag MONEY,
	@Kommentar TEXT,
	@BelegNr TEXT,
	@BelegPhotoName TEXT
AS
BEGIN
	UPDATE TableTrainingCost
	SET		TrainingDate = @TrainingDate,
			Betrag = @Betrag,
			Kommentar = @Kommentar,
			BelegNr = @BelegNr,
			BelegPhotoName = @BelegPhotoName		
	WHERE   TrainingCostID = @TrainingCostID
END