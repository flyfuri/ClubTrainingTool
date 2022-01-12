CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TablePilots.FirstName, TablePilots.LastName,
			TableDayPilotBuy.AxalpWeekFee, TableDayPilotBuy.DayFee, TableDayPilotBuy.YearFee, TableDayPilotBuy.Others, TableDayPilotBuy.Remarks
	FROM TableDayPilotBuy 
	INNER JOIN TableTrainings ON TableTrainings.TrainingID = TableDayPilotBuy.TrainingID
	INNER JOIN TblParticipants on TblParticipants.TrainingID = TableDayPilotBuy.TrainingID
	inner Join TablePilots on TablePilots.Pilot_ID = TblParticipants.PilotID
	WHERE YEAR(TableTrainings.TrainingDate)  = @YEAR 
	AND ((TableDayPilotBuy.AxalpWeekFee is not null and TableDayPilotBuy.AxalpWeekFee > 0)
	or (TableDayPilotBuy.DayFee is not null  AND TableDayPilotBuy.AxalpWeekFee > 0)
	or (TableDayPilotBuy.YearFee is not null and TableDayPilotBuy.YearFee > 0)
	or (TableDayPilotBuy.Others is not null and TableDayPilotBuy.Others > 0)
	or TableDayPilotBuy.Remarks is not null)
	
	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0