CREATE PROCEDURE [dbo].[Paricipants_delete]
	@ParticipantID INT
AS
BEGIN
	DELETE FROM TblParticipants 	
	WHERE   ParticipantID = @ParticipantID
END