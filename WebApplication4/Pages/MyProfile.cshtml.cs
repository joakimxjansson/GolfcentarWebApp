using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class MyProfileModel : PageModel
    {
        private readonly GolfContext _db;
        private readonly UserService _userService;
        public string Message { get; set; }
        public string Username { get; set; }
        public string UserImage { get; set; }
        public int Saldo { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public User User { get; set; }

        public MyProfileModel(GolfContext db, UserService userService)
        {
            _db = db;
            _userService = userService;
        }
        
        public void OnGet() {
            
            var id = HttpContext.Session.GetInt32("Id");
            User = _db.Users
                .Where(u => u.UserId == id.Value)
                .Include(u => u.Follower)
                .Include (u => u.Followee).FirstOrDefault();
            
                
            Username = _userService.GetUsername(id.Value);
            UserImage = _userService.GetImage(id.Value);
            Saldo = _userService.GetSaldo(id.Value);
            Email = _userService.GetEmail(id.Value);
            Name = _userService.GetName(id.Value);
            

            Message = "Välkommen " + Username + "!";

        }

        public IActionResult OnPostEditUsers()
        {
            return RedirectToPage("/Admin/EditCustomers");
        }


    }
}
