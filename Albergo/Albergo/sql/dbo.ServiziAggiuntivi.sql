CREATE TABLE [dbo].[ServiziAggiuntivi] (
    [ID]           INT             IDENTITY (1, 1) NOT NULL,
    [NomeServizio] VARCHAR (50)    NULL,
    [Descrizione]  TEXT            NULL,
    [Prezzo]       DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

