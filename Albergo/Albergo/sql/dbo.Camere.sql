CREATE TABLE [dbo].[Camere] (
    [Numero]               INT             NOT NULL,
    [Descrizione]          TEXT            NULL,
    [Tipologia]            VARCHAR (20)    NULL,
    [CaparraConfirmatoria] DECIMAL (10, 2) NULL,
    [TariffaApplicata]     DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([Numero] ASC)
);

