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
        CaparraConfirmatoria,
        TariffaApplicata,
        TipoSoggiorno
    )
    SELECT
        CodiceFiscaleCliente,
        NumeroCamera,
        DataPrenotazione,
        @MaxNumeroProgressivoAnno + ROW_NUMBER() OVER (ORDER BY (SELECT 1)),
        @CurrentYear,
        Dal,
        Al,
        CaparraConfirmatoria,
        TariffaApplicata,
        TipoSoggiorno
    FROM inserted;
END;
