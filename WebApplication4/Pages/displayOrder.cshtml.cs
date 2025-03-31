using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class displayOrderModel : PageModel
    {
        private readonly GolfContext _context;
        private readonly UserService _userService;

        public displayOrderModel(GolfContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public List<Order> Orders { get; set; } = new List<Order>();
        public int UserId { get; private set; }
        public string Username { get; private set; }

        public void OnGet()
        {
            var id = HttpContext.Session.GetInt32("Id");
            if (id != null)
            {
                UserId = id.Value;
                Username = _userService.GetUsername(UserId);

                // Retrieve orders for the logged-in user
                Orders = _context.Order
                    .Include(o => o.Product)
                    .Where(o => o.User.UserId == UserId)
                    .ToList();
            }
        }
    }
}
