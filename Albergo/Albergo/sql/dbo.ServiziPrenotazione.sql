CREATE TABLE [dbo].[ServiziPrenotazione] (
    [ID]             INT             IDENTITY (1, 1) NOT NULL,
    [IDPrenotazione] INT             NULL,
    [IDServizio]     INT             NULL,
    [Data]           DATE            NULL,
    [Quantita]       INT             NULL,
    [Prezzo]         DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([IDPrenotazione]) REFERENCES [dbo].[Prenotazioni] ([ID]),
    FOREIGN KEY ([IDServizio]) REFERENCES [dbo].[ServiziAggiuntivi] ([ID])
);

