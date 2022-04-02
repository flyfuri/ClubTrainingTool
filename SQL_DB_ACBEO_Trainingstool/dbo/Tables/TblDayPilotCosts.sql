CREATE TABLE [dbo].[TblDayPilotCosts] (
    [DayPilotCostID]    INT   IDENTITY (1, 1) NOT NULL,
    [TrainingID]        INT   NULL,
    [ParticipantID]     INT   NULL,
    [CostFlights]       MONEY NULL,
    [CostOtherServices] MONEY NULL,
    [CostBuy]           MONEY NULL,
    [PayWithAbo]        INT   NULL,
    [PayedAmount]       MONEY NULL,
    [PayedFlag]         BIT   NULL,
    [PayedTwint] MONEY NULL, 
    [PayedTwintReference] TEXT NULL, 
    PRIMARY KEY CLUSTERED ([DayPilotCostID] ASC)
);

