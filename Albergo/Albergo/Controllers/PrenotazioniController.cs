using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Albergo.DAO;
using Albergo.Models;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
public class PrenotazioniController : Controller
{
    private readonly IPrenotazioneDAO _prenotazioneDAO;
    private readonly IUserDAO _userDAO;
    private readonly ICameraDAO _cameraDAO;

    public PrenotazioniController(IPrenotazioneDAO prenotazioneDAO, IUserDAO userDAO, ICameraDAO cameraDAO)
    {
        _prenotazioneDAO = prenotazioneDAO;
        _userDAO = userDAO;
        _cameraDAO = cameraDAO;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var prenotazioni = await _prenotazioneDAO.GetPrenotazioniByUserIdAsync(userId);
        return View(prenotazioni);
    }

    public async Task<IActionResult> Create()
    {
        var model = new CreaPrenotazioneViewModel
        {
            CamereDisponibili = await _cameraDAO.GetAllCamereAsync()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreaPrenotazioneViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userDAO.GetUserByIdAsync(int.Parse(userId));
            var codiceFiscale = user.CodiceFiscale;

            var prenotazione = new Prenotazione
            {
                CodiceFiscaleCliente = codiceFiscale,
                NumeroCamera = model.NumeroCamera,
                Dal = model.DataInizio,
                Al = model.DataFine,
                TipoSoggiorno = model.TipoSoggiorno,
                UserId = user.ID
            };

            await _prenotazioneDAO.CreatePrenotazioneAsync(prenotazione);
            return RedirectToAction("Index");
        }

        model.CamereDisponibili = await _cameraDAO.GetAllCamereAsync();
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var prenotazione = await _prenotazioneDAO.GetPrenotazioneAsync(id);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (prenotazione == null || prenotazione.UserId != userId)
        {
            return NotFound();
        }
        return View(prenotazione);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Prenotazione prenotazione)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (id != prenotazione.ID || prenotazione.UserId != userId)
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
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (prenotazione == null || prenotazione.UserId != userId)
        {
            return NotFound();
        }
        return View(prenotazione);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var prenotazione = await _prenotazioneDAO.GetPrenotazioneAsync(id);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (prenotazione.UserId != userId)
        {
            return Unauthorized();
        }
        await _prenotazioneDAO.DeletePrenotazioneAsync(id);
        return RedirectToAction(nameof(Index));
    }
}