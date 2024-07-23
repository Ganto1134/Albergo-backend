using Albergo.Models;

public interface IPrenotazioneDAO
{
    Task CreatePrenotazioneAsync(Prenotazione prenotazione);
    Task<Prenotazione> GetPrenotazioneAsync(int id);
    Task<List<Prenotazione>> GetAllPrenotazioniAsync();
    Task UpdatePrenotazioneAsync(Prenotazione prenotazione);
    Task DeletePrenotazioneAsync(int id);
}

