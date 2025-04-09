using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class RegistrationModel : PageModel
    {

        private readonly GolfContext _db;
        private readonly UserService _userService;
        private readonly PasswordHasher _passwordHasher = new();

        public RegistrationModel(GolfContext db , UserService userService)
        {
            _db = db;
            _userService = userService;
        }
[BindProperty]
        public User User { get; set; } = new User();

        public string Message { get; set; }

        public IActionResult OnGet() {
            var id = HttpContext.Session.GetInt32("Id");
            if (id != null)
            {
            
            var role = _userService.GetRole(id.Value);
            if (role == 0) {
                return RedirectToPage("/MyProfile");

            }

            if (role == 1) {
                return RedirectToPage("/Admin/Adminpage");
            }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); //felaktig reg
            }
            if(_db.Users.Any(u => u.Username == User.Username))
            {
                Message = "Användernamn är redan i användning";
                return Page(); //Användare finns redan
            }
            User.Password = _passwordHasher.Hash(User.Password);
            _db.Users.Add(User);
            await _db.SaveChangesAsync();

            Message = "Du har registrerat dig!";
            return RedirectToPage("/Login"); //giltig reg, redirect till login-sida
        }
    }
}
