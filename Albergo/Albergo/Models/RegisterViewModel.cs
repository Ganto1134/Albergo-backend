using System.ComponentModel.DataAnnotations;
public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string Role { get; set; } = "User";

    [Required]
    public string CodiceFiscale { get; set; }

    public string Cognome { get; set; }
    public string Nome { get; set; }
    public string Citta { get; set; }
    public string Provincia { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public string Cellulare { get; set; }
}