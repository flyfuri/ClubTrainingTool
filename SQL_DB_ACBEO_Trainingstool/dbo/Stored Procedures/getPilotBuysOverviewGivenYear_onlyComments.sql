CREATE PROCEDURE [dbo].[getPilotBuysOverviewGivenYear_onlyComments]
	@YEAR INT
AS
	SELECT TableTrainings.TrainingDate, TablePilots.FirstName, TablePilots.LastName,
			TableDayPilotBuy.AxalpWeekFee, TableDayPilotBuy.DayFee, TableDayPilotBuy.YearFee, TableDayPilotBuy.Others, TableDayPilotBuy.Remarks
	FROM TableDayPilotBuy 
	INNER JOIN TableTrainings ON TableTrainings.TrainingID = TableDayPilotBuy.TrainingID
	INNER JOIN TblParticipants on TblParticipants.TrainingID = TableDayPilotBuy.TrainingID
	inner Join TablePilots on TablePilots.Pilot_ID = TblParticipants.PilotID
	WHERE YEAR(TableTrainings.TrainingDate)  = @YEAR 
	AND ((TableDayPilotBuy.AxalpWeekFee is null or TableDayPilotBuy.AxalpWeekFee = 0.00)
	and (TableDayPilotBuy.DayFee is null or TableDayPilotBuy.DayFee = 0.00)
	and (TableDayPilotBuy.YearFee is null or TableDayPilotBuy.YearFee = 0.00)
	and (TableDayPilotBuy.Others is null or TableDayPilotBuy.Others = 0.00)
	and (TableDayPilotBuy.LifewestBuy is null or TableDayPilotBuy.LifewestBuy = 0.00)
	and (TableDayPilotBuy.LifewestRent is null  or TableDayPilotBuy.LifewestRent = 0.00)
	and (TableDayPilotBuy.Others is null or TableDayPilotBuy.Others = 0.00)
	and TableDayPilotBuy.Remarks is not null)
	
	ORDER BY TableTrainings.TrainingDate ASC
RETURN 0