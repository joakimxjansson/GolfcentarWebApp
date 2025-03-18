using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public MyProfileModel(GolfContext db, UserService userService)
        {
            _db = db;
            _userService = userService;
        }
        
        public void OnGet()
        {
            var id = HttpContext.Session.GetInt32("Id");
            Username = _userService.GetUsername(id.Value);
            Message = "Välkommen " + Username + "!";

        }

        
    }
}
