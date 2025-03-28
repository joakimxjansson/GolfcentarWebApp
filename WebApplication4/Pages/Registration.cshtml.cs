using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;

namespace WebApplication4.Pages
{
    public class RegistrationModel : PageModel
    {

        private readonly GolfContext _db;

        public RegistrationModel(GolfContext db)
        {
            _db = db;
        }
[BindProperty]
        public User User { get; set; } = new User();

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); //felaktig reg
            }
            if(_db.Users.Any(u => u.Username == User.Username))
            {
                Message = "Användarnamn är redan i användning!";
                return Page(); //Användare finns redan
            }

            _db.Users.Add(User);
            await _db.SaveChangesAsync();

            Message = "Du har registrerat dig!";
            return RedirectToPage("/Login"); //giltig reg, redirect till login-sida
        }
    }
}
