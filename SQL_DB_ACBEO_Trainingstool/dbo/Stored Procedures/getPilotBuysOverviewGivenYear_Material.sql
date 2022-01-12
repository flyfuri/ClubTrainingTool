CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_Material]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TableDayPilotBuy.ParticipantID, TablePilots.FirstName, TablePilots.LastName,
			TableDayPilotBuy.LifewestBuy, TableDayPilotBuy.LifewestRent, TableDayPilotBuy.Others, TableDayPilotBuy.Remarks
	FROM TableDayPilotBuy 
	FULL JOIN TableTrainings ON TableDayPilotBuy.TrainingID = TableTrainings.TrainingID
	FULL Join TblParticipants on TblParticipants.ParticipantID = TableDayPilotBuy.ParticipantID
	FULL join TablePilots on TablePilots.Pilot_ID = TblParticipants.PilotID
	WHERE ((TableDayPilotBuy.LifewestBuy is not null AND TableDayPilotBuy.LifewestBuy <> 0.00)
	OR (TableDayPilotBuy.LifewestRent is not null  AND TableDayPilotBuy.LifewestRent <> 0.00)
	OR (TableDayPilotBuy.Others is not null AND TableDayPilotBuy.Others <> 0.00)
	/*OR (TableDayPilotBuy.Remarks is not null AND TableDayPilotBuy.Remarks <> ''))*/)
	AND YEAR(TableTrainings.TrainingDate)  = @YEAR

	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0