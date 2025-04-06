using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Pages.UserActions
{
    public class ForgottenUserNameModel : PageModel
    {
        private readonly GolfContext _db;
        
        public ForgottenUserNameModel(GolfContext db)
        {
            _db = db;
        }

        [BindProperty, EmailAddress(ErrorMessage = "Det bör vara en giltig E-mail adress")]
        public string EmailAddress { get; set; }
        public string? UserName { get; set; }
        public bool MailRecived { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {            
            if (ModelState.IsValid)
            {
                MailRecived = true;
                var user = _db.Users.FirstOrDefault(u => u.Email == EmailAddress);
                if (user is not null)
                {
                    UserName = user.Username;
                }
                else
                {
                    UserName = null;
                }
            }              
            return Page();
        }
    }
}
