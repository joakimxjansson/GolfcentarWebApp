using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages {
    public class LoginModel : PageModel {
        private readonly GolfContext _db;
        private readonly UserService _userService;
        private readonly PasswordHasher _passwordHasher = new();

        public LoginModel(GolfContext db, UserService userService) {
            _db = db;
            _userService = userService;
        }

        [BindProperty] public string UserName { get; set; }

        [BindProperty] public string PassWord { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet() {
            var id = HttpContext.Session.GetInt32("Id");
            if (id != null) {
                var role = _userService.GetRole(id.Value);
                if (role == 1) {
                    return RedirectToPage("/Admin/AdminPage");
                }

                return RedirectToPage("/MyProfile");
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var user = _db.Users.FirstOrDefault(u => u.Username == UserName);

            if (user != null && _passwordHasher.Verify(PassWord, user.Password)) {
                HttpContext.Session.SetInt32("Id", user.UserId); //skapar session
                if (user.Admin == 0) {
                    return RedirectToPage("/MyProfile");
                }

                return RedirectToPage("/Admin/AdminPage"); //redirect till ny sida
            } else {
                Message = "Fel användarnamn eller lösenord! Försök igen";
                return Page();
            }
        }
    }
}