CREATE TABLE [dbo].[TableAbos] (
    [Abo_ID]       INT          IDENTITY (1, 1) NOT NULL,
    [PilotID]      INT          NULL,
    [Abo_Nr]       VARCHAR (50) NULL,
    [DateBought]   DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight1]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight2]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight3]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight4]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight5]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight6]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight7]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight8]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight9]  DATE         DEFAULT ('1753-01-01') NOT NULL,
    [DateFlight10] DATE         DEFAULT ('1753-01-01') NOT NULL,
    PRIMARY KEY CLUSTERED ([Abo_ID] ASC)
);

