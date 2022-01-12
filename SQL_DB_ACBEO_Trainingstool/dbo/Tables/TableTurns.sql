CREATE TABLE [dbo].[TableTurns] (
    [TurnID]        INT  IDENTITY (1, 1) NOT NULL,
    [TrainingID]    INT  NULL,
    [ParticipantID] INT  NULL,
    [TurnNr]        INT  NULL,
    [Flight]        TEXT NULL,
    PRIMARY KEY CLUSTERED ([TurnID] ASC)
);

