CREATE TABLE [dbo].[TableTrainingCost] (
    [TrainingCostID] INT   IDENTITY (1, 1) NOT NULL,
    [TrainingDate]   DATE  NULL,
    [Betrag]         MONEY NULL,
    [Kommentar]      TEXT  NULL,
    [BelegNr]        TEXT  NULL,
    [BelegPhotoName] TEXT  NULL,
    PRIMARY KEY CLUSTERED ([TrainingCostID] ASC)
);

