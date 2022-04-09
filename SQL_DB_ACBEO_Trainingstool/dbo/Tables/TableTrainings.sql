CREATE TABLE [dbo].[TableTrainings] (
    [TrainingID]   INT   IDENTITY (1, 1) NOT NULL,
    [TrainingDate] DATE  NOT NULL,
    [CashAtBegin]  MONEY NULL,
    [CashToACBEO]  MONEY NULL,
    [CashAtEnd]    MONEY NULL,
    [Remarks]      TEXT  NULL,
    [Leiter1_ID]   INT   NULL,
    [Leiter2_ID]   INT   NULL,
    [CashToACBEO_payedBy] TEXT NULL, 
    [PayedTwintReference] TEXT NULL, 
    PRIMARY KEY CLUSTERED ([TrainingID] ASC)
);

