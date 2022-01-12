CREATE PROCEDURE [dbo].[Participants_delete]
	@ParticipantID INT
AS
BEGIN
	DELETE FROM TblParticipants 	
	WHERE   ParticipantID = @ParticipantID
END