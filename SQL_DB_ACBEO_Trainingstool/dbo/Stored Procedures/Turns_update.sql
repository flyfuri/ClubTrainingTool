CREATE PROCEDURE [dbo].[Turns_update]
	@TurnID INT,
	@TrainingID INT,
	@ParticipantID INT,
	@TurnNr INT,
	@Flight TEXT
AS
BEGIN
	UPDATE TableTurns 	
	SET		TrainingID = @TrainingID, 
			ParticipantID = @ParticipantID,
			TurnNr = @TurnNr,
			Flight = @Flight

	WHERE   TurnID = @TurnID
END