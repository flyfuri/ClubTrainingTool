CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_JobMoneyPayed]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TablePilots.FirstName, TablePilots.LastName, TblDayPilotCosts.PayedAmount
	FROM TableTrainings
	full join TblDayPilotCosts on TableTrainings.TrainingID = TblDayPilotCosts.TrainingID
	FULL Join TblParticipants on TblParticipants.ParticipantID = TblDayPilotCosts.ParticipantID
	FULL join TablePilots on TablePilots.Pilot_ID = TblParticipants.PilotID
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR
	and TblDayPilotCosts.PayedAmount < 0

	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0