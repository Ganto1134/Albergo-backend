namespace Albergo.Models
{
    public class ServizioPrenotazione
    {
        public int ID { get; set; }
        public int IDPrenotazione { get; set; } 
        public int IDServizio { get; set; } 
        public DateTime Data { get; set; } 
        public int Quantita { get; set; } 
        public decimal Prezzo { get; set; } 
    }
}
