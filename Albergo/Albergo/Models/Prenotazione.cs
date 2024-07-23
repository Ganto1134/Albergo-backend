namespace Albergo.Models
{
    public class Prenotazione
    {
        public int ID { get; set; }
        public string CodiceFiscaleCliente { get; set; }
        public int NumeroCamera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumeroProgressivoAnno { get; set; }
        public int Anno { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal TariffaApplicata { get; set; }
        public string TipoSoggiorno { get; set; }
    }
}
