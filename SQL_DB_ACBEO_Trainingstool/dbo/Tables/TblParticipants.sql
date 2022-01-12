CREATE TABLE [dbo].[TblParticipants] (
    [ParticipantID] INT IDENTITY (1, 1) NOT NULL,
    [TrainingID]    INT NULL,
    [PilotID]       INT NULL,
    PRIMARY KEY CLUSTERED ([ParticipantID] ASC),
    CONSTRAINT [FK_TblParticipants_TablePilots] FOREIGN KEY ([PilotID]) REFERENCES [dbo].[TablePilots] ([Pilot_ID])
);

