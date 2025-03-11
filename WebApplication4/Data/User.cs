using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Data;

public class User {
    public int UserId { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    public int Admin { get; set; } = 0;
    public int Saldo { get; set; } = 1000;
    public string? UserImage { get; set; }
}