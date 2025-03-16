using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;

namespace WebApplication4.Pages
{
    public class LoginModel : PageModel
    {
        private readonly GolfContext _db;
        
        public LoginModel(GolfContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string PassWord { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _db.User.FirstOrDefault(u => u.Username == UserName && u.Password == PassWord);

            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Username); //skapar session

                return RedirectToPage("/..."); //redirect till ny sida
            }
            else
            {
                Message = "Fel användarnamn eller lösenord! Försök igen";
                return Page();
            }
        }
    }
}
