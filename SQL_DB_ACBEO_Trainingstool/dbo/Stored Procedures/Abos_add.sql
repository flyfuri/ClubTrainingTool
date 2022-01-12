CREATE PROCEDURE [dbo].[Abos_add]
	@PilotID INT,
	@Abo_Nr  VARCHAR(50)
AS
BEGIN
	INSERT INTO TableAbos(PilotID, Abo_Nr, DateBought) 
	VALUES (@PilotID, @Abo_Nr, GETDATE())
END