CREATE TABLE [dbo].[TableDayPilotBuy] (
    [DayPilotBuyID] INT   IDENTITY (1, 1) NOT NULL,
    [TrainingID]    INT   NULL,
    [ParticipantID] INT   NULL,
    [LifewestBuy]   MONEY NULL,
    [LifewestRent]  MONEY NULL,
    [YearFee]       MONEY NULL,
    [DayFee]        MONEY NULL,
    [AxalpWeekFee]  MONEY NULL,
    [Others]        MONEY NULL,
    [Remarks]       TEXT  NULL,
    PRIMARY KEY CLUSTERED ([DayPilotBuyID] ASC)
);

