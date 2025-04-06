using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication4.Data;

public class User {
    public int UserId { get; set; }
    [Required(ErrorMessage = "Du b�r fylla i detta f�lt!"), 
        MaxLength(15, ErrorMessage = "Du f�r enbart anv�nda up till 15 tecken!"), 
        MinLength(4, ErrorMessage = "Du b�r anv�nda minst 4 tecken!")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Du b�r fylla i detta f�lt!")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Du b�r fylla i detta f�lt!"),
        EmailAddress(ErrorMessage = "Det b�r vara en giltig mailadress.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Du b�r fylla i detta f�lt!")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Du b�r fylla i detta f�lt!")]
    public string LastName { get; set; }

    public int Admin { get; set; } = 0;
    public int Saldo { get; set; } = 10000;
    public string? UserImage { get; set; } = "/images/DefaultImage.png";
    
    public ICollection<Follow>? Follower { get; set; }

    public ICollection<Follow>? Followee { get; set; }
}