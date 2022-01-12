CREATE PROCEDURE [dbo].[Turns_add]
	@TrainingID INT,
	@ParticipantID INT,
	@TurnNr INT,
	@Flight TEXT
AS
BEGIN
	INSERT INTO TableTurns(TrainingID, ParticipantID, TurnNr, Flight) 
	VALUES (@TrainingID, @ParticipantID, @TurnNr, @Flight)
END