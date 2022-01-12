CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_LeiterJobs]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TableTrainings.Leiter1_ID, TableTrainings.Leiter2_ID
	FROM TableTrainings
	/*FULL Join TblParticipants on TblParticipants.ParticipantID = TblDayPilotCosts.ParticipantID
	FULL join TablePilots on TablePilots.Pilot_ID = TblParticipants.PilotID*/
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR

	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0