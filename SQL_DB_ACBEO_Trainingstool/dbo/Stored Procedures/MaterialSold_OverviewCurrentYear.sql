CREATE PROCEDURE [dbo].[MaterialSold_OverviewCurrentYear]
AS
	SELECT LifewestBuy, Others, TableDayPilotBuy.Remarks
	from TableDayPilotBuy 
	inner join TableTrainings on TableTrainings.TrainingID = TableDayPilotBuy.TrainingID
	where  YEAR(TrainingDate) = YEAR(GETDATE()-1)
	and LifewestBuy <> NULL;
RETURN 0