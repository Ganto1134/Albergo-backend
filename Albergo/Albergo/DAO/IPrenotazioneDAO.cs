using Albergo.Models;

namespace Albergo.DAO {
    public interface IPrenotazioneDAO
    {
        Task CreatePrenotazioneAsync(Prenotazione prenotazione);
        Task<Prenotazione> GetPrenotazioneAsync(int id);
        Task<List<Prenotazione>> GetAllPrenotazioniAsync();
        Task UpdatePrenotazioneAsync(Prenotazione prenotazione);
        Task DeletePrenotazioneAsync(int id);
        Task<List<Prenotazione>> GetPrenotazioniByCodiceFiscaleAsync(string codiceFiscale);
        Task<List<ServizioAggiuntivo>> GetServiziAggiuntiviAsync();
        Task AggiungiServizioAsync(ServizioPrenotazione servizioPrenotazione);
    }
}