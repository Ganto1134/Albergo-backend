using Albergo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Albergo.DAO;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
public class AccountController : Controller
{
    private readonly IUserDAO _userDAO;

    public AccountController(IUserDAO userDAO)
    {
        _userDAO = userDAO;
    }

    // Azione di login
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userDAO.GetUserByUsernameAsync(model.Username);
            if (user != null)
            {
                Console.WriteLine($"DEBUG: DB PasswordHash: {user.PasswordHash}");
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
                Console.WriteLine($"DEBUG: IsPasswordValid: {isPasswordValid}");

                if (isPasswordValid)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid password.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username.");
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userDAO.GetUserByUsernameAsync(model.Username);
            if (existingUser == null)
            {
                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Role = "User",
                    CreatedAt = DateTime.UtcNow,
                    CodiceFiscale = model.CodiceFiscale,
                    Cognome = model.Cognome,
                    Nome = model.Nome,
                    Citta = model.Citta,
                    Provincia = model.Provincia,
                    Email = model.Email,
                    Telefono = model.Telefono,
                    Cellulare = model.Cellulare
                };

                await _userDAO.CreateUserAsync(user);
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError(string.Empty, "Username già esistente.");
        }
        return View(model);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}