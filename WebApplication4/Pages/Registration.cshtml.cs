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

        public User User { get; set; }

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

            _db.User.Add(User);
            await _db.SaveChangesAsync();

            Message = "Du har registrerat dig!";
            return RedirectToPage("/Login"); //giltig reg, redirect till login-sida
        }

        
    }
}
