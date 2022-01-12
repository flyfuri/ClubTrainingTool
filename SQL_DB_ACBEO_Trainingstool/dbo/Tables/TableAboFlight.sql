CREATE TABLE [dbo].[TableAboFlight] (
    [AboFlight_ID]        INT  IDENTITY (1, 1) NOT NULL,
    [PilotID]             INT  NULL,
    [Abo_Year]            INT  NULL,
    [Abo_NrInYear]        INT  NULL,
    [FlightNrOnAbo]       INT  NULL,
    [DateBought]          DATE DEFAULT (getdate()) NULL,
    [DateFlightPayedWith] DATE DEFAULT ('1753-01-01') NOT NULL,
    [Comment]             TEXT NULL,
    [SellerID]            INT  NULL,
    CONSTRAINT [PK_TableAboFlight] PRIMARY KEY CLUSTERED ([AboFlight_ID] ASC)
);

