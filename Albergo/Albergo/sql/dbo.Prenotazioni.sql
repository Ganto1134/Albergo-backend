CREATE TABLE [dbo].[Prenotazioni] (
    [ID]                    INT          IDENTITY (1, 1) NOT NULL,
    [CodiceFiscaleCliente]  VARCHAR (16) NULL,
    [NumeroCamera]          INT          NULL,
    [DataPrenotazione]      DATE         NULL,
    [NumeroProgressivoAnno] INT          NULL,
    [Anno]                  INT          NULL,
    [Dal]                   DATE         NULL,
    [Al]                    DATE         NULL,
    [TipoSoggiorno]         VARCHAR (50) NULL,
    [UserId]                INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([NumeroCamera]) REFERENCES [dbo].[Camere] ([Numero]),
    CONSTRAINT [FK_Prenotazioni_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([ID])
);


GO
CREATE TRIGGER trg_UpdateAnnoAndNumeroProgressivo
ON dbo.Prenotazioni
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @CurrentYear INT = YEAR(GETDATE());
    DECLARE @MaxNumeroProgressivoAnno INT;

    SELECT @MaxNumeroProgressivoAnno = ISNULL(MAX(NumeroProgressivoAnno), 0)
    FROM dbo.Prenotazioni
    WHERE Anno = @CurrentYear;

    INSERT INTO dbo.Prenotazioni (
        CodiceFiscaleCliente,
        NumeroCamera,
        DataPrenotazione,
        NumeroProgressivoAnno,
        Anno,
        Dal,
        Al,
        TipoSoggiorno,
        UserId
    )
    SELECT
        CodiceFiscaleCliente,
        NumeroCamera,
        DataPrenotazione,
        @MaxNumeroProgressivoAnno + ROW_NUMBER() OVER (ORDER BY (SELECT 1)),
        @CurrentYear,
        Dal,
        Al,
        TipoSoggiorno,
        UserId
    FROM inserted;
END;