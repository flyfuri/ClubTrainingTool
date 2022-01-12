CREATE PROCEDURE [dbo].[Participants_deleteFromAllTables]
	@ParticipantID INT
AS
BEGIN
	DELETE FROM TblParticipants 	
	WHERE   ParticipantID = @ParticipantID

	DELETE FROM TableTurns 	
	WHERE   ParticipantID = @ParticipantID

	DELETE FROM TableDayPilotBuy 	
	WHERE   ParticipantID = @ParticipantID

	DELETE FROM TblDayPilotCosts 	
	WHERE   ParticipantID = @ParticipantID
	
END