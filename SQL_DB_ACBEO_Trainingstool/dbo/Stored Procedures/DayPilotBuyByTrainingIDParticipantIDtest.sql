CREATE PROCEDURE [dbo].[DayPilotBuyByTrainingIDParticipantIDtest]
	@TraingingID int,
	@ParticipantID int
AS
	SELECT TOP 1 TableDayPilotBuy.*, TblParticipants.PilotID, TableAbos.Abo_Nr
	FROM TableDayPilotBuy
	INNER JOIN TblParticipants ON TableDayPilotBuy.ParticipantID = TblParticipants.ParticipantID
	INNER JOIN TableAbos ON TblParticipants.PilotID = TableAbos.PilotID
	WHERE TableDayPilotBuy.TrainingID = @TraingingID 
	AND TableDayPilotBuy.ParticipantID = @ParticipantID
	AND (DateFlight1 IS NULL
	OR DateFlight2 IS NULL
	OR DateFlight3 IS NULL
	OR DateFlight4 IS NULL
	OR DateFlight5 IS NULL
	OR DateFlight6 IS NULL
	OR DateFlight7 IS NULL
	OR DateFlight8 IS NULL
	OR DateFlight9 IS NULL
	OR DateFlight10 IS NULL)
	ORDER BY TableAbos.Abo_Nr DESC
RETURN 0