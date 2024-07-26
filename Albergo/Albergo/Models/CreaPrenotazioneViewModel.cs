namespace Albergo.Models
{
    public class CreaPrenotazioneViewModel
    {
        public int NumeroCamera { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public string TipoSoggiorno { get; set; }
        public List<Camera> CamereDisponibili { get; set; }
    }
}
