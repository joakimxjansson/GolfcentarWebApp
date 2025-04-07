using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication4.Data;

public class User {
    public int UserId { get; set; }
    [Required(ErrorMessage = "Du bör fylla i detta fält!"), 
        MaxLength(15, ErrorMessage = "Du får enbart använda up till 15 tecken!"), 
        MinLength(4, ErrorMessage = "Du bör använda minst 4 tecken!")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Du bör fylla i detta fält!")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Du bör fylla i detta fält!"),
        EmailAddress(ErrorMessage = "Det bör vara en giltig mailadress.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Du bör fylla i detta fält!")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Du bör fylla i detta fält!")]
    public string LastName { get; set; }

    public int Admin { get; set; } = 0;
    public int Saldo { get; set; } = 10000;
    public string? UserImage { get; set; } = "/images/DefaultImage.png";
    
    public ICollection<Follow>? Follower { get; set; }

    public ICollection<Follow>? Followee { get; set; }
}