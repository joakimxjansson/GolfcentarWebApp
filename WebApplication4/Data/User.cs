using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication4.Data;

public class User {
    public int UserId { get; set; }
    [Required(ErrorMessage = "Anv�ndar namn: Du b�r fylla i detta f�lt!"), 
        MaxLength(15, ErrorMessage = "Anv�ndar namn: Du f�r enbart anv�nda 15 bokst�ver!"), 
        MinLength(4, ErrorMessage = "Anv�ndar namn: Du b�r anv�nda 4 eller men bokst�ver!")]
    public string Username { get; set; }
    [Required(ErrorMessage = "L�senord: Du b�r fylla i detta f�lt!")]
    public string Password { get; set; }
    [Required(ErrorMessage = "E-Mail: Du b�r fylla i detta f�lt!"),
        EmailAddress(ErrorMessage = "E-Mail: Det b�r vara en giltig mailadress.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "F�rnamn: Du b�r fylla i detta f�lt!")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Efternamn: Du b�r fylla i detta f�lt!")]
    public string LastName { get; set; }

    public int Admin { get; set; } = 0;
    public int Saldo { get; set; } = 1000;
    public string? UserImage { get; set; }
}