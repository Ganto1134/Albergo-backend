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
}

