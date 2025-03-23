using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication4.Data;

public class User {
    public int UserId { get; set; }
    [Required(ErrorMessage = "Användar namn: Du bör fylla i detta fält!"), 
        MaxLength(15, ErrorMessage = "Användar namn: Du får enbart använda 15 bokstäver!"), 
        MinLength(4, ErrorMessage = "Användar namn: Du bör använda 4 eller men bokstäver!")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Lösenord: Du bör fylla i detta fält!")]
    public string Password { get; set; }
    [Required(ErrorMessage = "E-Mail: Du bör fylla i detta fält!"),
        EmailAddress(ErrorMessage = "E-Mail: Det bör vara en giltig mailadress.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Förnamn: Du bör fylla i detta fält!")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Efternamn: Du bör fylla i detta fält!")]
    public string LastName { get; set; }

    public int Admin { get; set; } = 0;
    public int Saldo { get; set; } = 1000;
    public string? UserImage { get; set; }
}