using Albergo.Models;

namespace Albergo.DAO {
    public interface IPrenotazioneDAO
    {
        Task CreatePrenotazioneAsync(Prenotazione prenotazione);
        Task<Prenotazione> GetPrenotazioneAsync(int id);
        Task<List<Prenotazione>> GetPrenotazioniByUserIdAsync(int userId);
        Task UpdatePrenotazioneAsync(Prenotazione prenotazione);
        Task DeletePrenotazioneAsync(int id);
        Task<List<Camera>> GetAllCamereAsync();
    }
}