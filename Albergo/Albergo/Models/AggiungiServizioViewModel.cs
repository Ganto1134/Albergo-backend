namespace Albergo.Models
{
    public class AggiungiServizioViewModel
    {
        public int PrenotazioneId { get; set; }
    public int ServizioId { get; set; }
    public int Quantita { get; set; }
    public decimal Prezzo { get; set; }
    public List<ServizioAggiuntivo> ServiziDisponibili { get; set; }
    }
}
