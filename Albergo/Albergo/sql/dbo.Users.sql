CREATE TABLE [dbo].[Users] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Username]      NVARCHAR (50)  NOT NULL,
    [PasswordHash]  NVARCHAR (555) NOT NULL,
    [Role]          NVARCHAR (20)  NOT NULL,
    [CreatedAt]     DATETIME       DEFAULT (getdate()) NULL,
    [CodiceFiscale] VARCHAR (16)   NOT NULL,
    [Cognome]       VARCHAR (50)   NULL,
    [Nome]          VARCHAR (50)   NULL,
    [Citta]         VARCHAR (50)   NULL,
    [Provincia]     VARCHAR (50)   NULL,
    [Email]         VARCHAR (100)  NULL,
    [Telefono]      VARCHAR (20)   NULL,
    [Cellulare]     VARCHAR (20)   NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

