CREATE TABLE [dbo].[TablePilots] (
    [Pilot_ID]     INT  IDENTITY (1, 1) NOT NULL,
    [LicensNr]     TEXT NULL,
    [FirstName]    TEXT NULL,
    [LastName]     TEXT NULL,
    [Disactivated] BIT  NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([Pilot_ID] ASC)
);

