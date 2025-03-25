using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class RegistrationModel : PageModel
    {

        private readonly GolfContext _db;
        private readonly UserService _userService;

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
            var role = _userService.GetRole(id.Value);
            if (role == 0) {
                return RedirectToPage("/MyProfile");

            }

            if (role == 1) {
                return RedirectToPage("/Admin/Adminpage");
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

            _db.Users.Add(User);
            await _db.SaveChangesAsync();

            Message = "Du har registrerat dig!";
            return RedirectToPage("/Login"); //giltig reg, redirect till login-sida
        }
    }
}
