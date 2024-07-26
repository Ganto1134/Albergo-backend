using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Albergo.DAO;

[Authorize]
public class AdminController : Controller
{
    private readonly IPrenotazioneDAO _prenotazioneDAO;

    public AdminController(IPrenotazioneDAO prenotazioneDAO)
    {
        _prenotazioneDAO = prenotazioneDAO;
    }

    public async Task<IActionResult> Index(string codiceFiscale)
    {
        var prenotazioni = string.IsNullOrEmpty(codiceFiscale)
            ? await _prenotazioneDAO.GetAllPrenotazioniAsync()
            : await _prenotazioneDAO.GetPrenotazioniByCodiceFiscaleAsync(codiceFiscale);

        return View(prenotazioni);
    }
}

