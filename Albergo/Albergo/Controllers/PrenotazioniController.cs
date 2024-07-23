using Albergo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class PrenotazioniController : Controller
{
    private readonly IPrenotazioneDAO _prenotazioneDAO;

    public PrenotazioniController(IPrenotazioneDAO prenotazioneDAO)
    {
        _prenotazioneDAO = prenotazioneDAO;
    }

    public async Task<IActionResult> Index()
    {
        var prenotazioni = await _prenotazioneDAO.GetAllPrenotazioniAsync();
        return View(prenotazioni);
    }

    public async Task<IActionResult> Details(int id)
    {
        var prenotazione = await _prenotazioneDAO.GetPrenotazioneAsync(id);
        if (prenotazione == null)
        {
            return NotFound();
        }
        return View(prenotazione);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Prenotazione prenotazione)
    {
        if (ModelState.IsValid)
        {
            await _prenotazioneDAO.CreatePrenotazioneAsync(prenotazione);
            return RedirectToAction(nameof(Index));
        }
        return View(prenotazione);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var prenotazione = await _prenotazioneDAO.GetPrenotazioneAsync(id);
        if (prenotazione == null)
        {
            return NotFound();
        }
        return View(prenotazione);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Prenotazione prenotazione)
    {
        if (id != prenotazione.ID)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            await _prenotazioneDAO.UpdatePrenotazioneAsync(prenotazione);
            return RedirectToAction(nameof(Index));
        }
        return View(prenotazione);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var prenotazione = await _prenotazioneDAO.GetPrenotazioneAsync(id);
        if (prenotazione == null)
        {
            return NotFound();
        }
        return View(prenotazione);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _prenotazioneDAO.DeletePrenotazioneAsync(id);
        return RedirectToAction(nameof(Index));
    }
}