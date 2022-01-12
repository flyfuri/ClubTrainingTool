CREATE PROCEDURE [dbo].[ParticipantsByTrainingID]
	@TrainingID int
AS
	SELECT TblParticipants.ParticipantID, TblParticipants.TrainingID, TblParticipants.PilotID, TablePilots.FirstName, TablePilots.LastName, TablePilots.LicensNr
	FROM TblParticipants 
	INNER JOIN TablePilots ON TblParticipants.PilotID = TablePilots.Pilot_ID
	WHERE TblParticipants.TrainingID = @TrainingID
RETURN 0