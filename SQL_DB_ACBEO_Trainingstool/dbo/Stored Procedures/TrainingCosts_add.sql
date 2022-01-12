CREATE PROCEDURE [dbo].[TrainingCosts_add]
	@TrainingDate DateTime,
	@Betrag MONEY,
	@Kommentar TEXT,
	@BelegNr TEXT,
	@BelegPhotoName TEXT
AS
BEGIN
	INSERT INTO TableTrainingCost(TrainingDate, Betrag, Kommentar, BelegNr, BelegPhotoName) 
	VALUES (@TrainingDate, @Betrag, @Kommentar, @BelegNr, @BelegPhotoName )
END