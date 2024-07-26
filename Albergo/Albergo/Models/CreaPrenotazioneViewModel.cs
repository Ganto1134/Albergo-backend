namespace Albergo.Models
{
    public class CreaPrenotazioneViewModel
    {
        public string CodiceFiscaleCliente { get; set; }
        public int NumeroCamera { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public string TipoSoggiorno { get; set; }
        public IEnumerable<Camera> CamereDisponibili { get; set; }
    }
}
