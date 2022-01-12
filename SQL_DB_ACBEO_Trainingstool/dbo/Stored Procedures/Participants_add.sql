CREATE PROCEDURE [dbo].[Participants_add]
	@TrainingID int,
	@PilotID int
AS
BEGIN
	INSERT INTO TblParticipants (TrainingID, PilotID) 
	VALUES (@TrainingID, @PilotID)
END