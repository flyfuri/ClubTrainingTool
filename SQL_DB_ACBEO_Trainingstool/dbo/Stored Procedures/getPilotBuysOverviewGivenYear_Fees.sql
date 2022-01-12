CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_Fees]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TablePilots.FirstName, TablePilots.LastName,
			TableDayPilotBuy.AxalpWeekFee, TableDayPilotBuy.DayFee, TableDayPilotBuy.YearFee, TableDayPilotBuy.Remarks
	FROM TableDayPilotBuy 
	FULL JOIN TableTrainings ON TableTrainings.TrainingID = TableDayPilotBuy.TrainingID
	FULL JOIN TblParticipants on TblParticipants.ParticipantID = TableDayPilotBuy.ParticipantID
	FULL Join TablePilots on TablePilots.Pilot_ID = TblParticipants.PilotID
	WHERE ((TableDayPilotBuy.AxalpWeekFee is not null and TableDayPilotBuy.AxalpWeekFee <> 0.00)
	or (TableDayPilotBuy.DayFee is not null  AND TableDayPilotBuy.DayFee <> 0.00)
	or (TableDayPilotBuy.YearFee is not null and TableDayPilotBuy.YearFee <> 0.00)
	/*or (TableDayPilotBuy.Others is not null and TableDayPilotBuy.Others <> 0.00)*/)
	and YEAR(TableTrainings.TrainingDate)  = @YEAR 

	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0