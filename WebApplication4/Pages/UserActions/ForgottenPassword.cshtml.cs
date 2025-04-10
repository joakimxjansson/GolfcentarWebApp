using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;

namespace WebApplication4.Pages.UserActions
{
    public class ForgottenPasswordModel : PageModel
    {
        private readonly GolfContext _db;
        private readonly PasswordHasher _passwordHasher = new();

        public ForgottenPasswordModel(GolfContext db)
        {
            _db = db;
        }

        [BindProperty, EmailAddress(ErrorMessage = "Det bör vara en giltig E-mail adress")]
        public string EmailAddress { get; set; }
        public int? UserID { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }

        public bool MailRecived { get; set; }
        public bool SecondMailRecived { get; set; }

        public async Task OnPostMailAsync()
        {
            MailRecived = true;
            var user = _db.Users.FirstOrDefault(u => u.Email == EmailAddress);
            if (user is not null)
            {
                UserID = user.UserId;
            }
            else
            {
                UserID = null;
            }

        }

        public async Task<IActionResult> OnPostPasswordAsync(int id)
        {
            _db.Users.First(u => u.UserId == id).Password = _passwordHasher.Hash(NewPassword);
            await _db.SaveChangesAsync();
            MailRecived = true;
            SecondMailRecived = true;
            return Page();
        }
    }
}
