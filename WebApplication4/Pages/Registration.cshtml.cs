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
                Message = "Försök igen";
                return Page(); //felaktig reg
            }
            if (string.IsNullOrEmpty(User.Email))
            {
                Message = "Email är obligatoriskt!";
                return Page();
            }

            _db.Users.Add(User);
            await _db.SaveChangesAsync();

            Message = "Du har registrerat dig!";
            return RedirectToPage("/Login"); //giltig reg, redirect till login-sida
        }

        
    }
}
