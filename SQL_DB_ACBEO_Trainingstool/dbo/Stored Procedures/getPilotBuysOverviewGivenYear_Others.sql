CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_Others]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TablePilots.FirstName, TablePilots.LastName,
			TableDayPilotBuy.Others, TableDayPilotBuy.Remarks
	FROM TableDayPilotBuy 
	FULL JOIN TableTrainings ON TableTrainings.TrainingID = TableDayPilotBuy.TrainingID
	FULL JOIN TblParticipants ON TblParticipants.ParticipantID = TableDayPilotBuy.ParticipantID
	FULL JOIN TablePilots ON TablePilots.Pilot_ID = TblParticipants.PilotID
	WHERE YEAR(TableTrainings.TrainingDate) = @YEAR 
	AND (TableDayPilotBuy.Others IS NOT NULL AND TableDayPilotBuy.Others <> 0.00)
	
	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0