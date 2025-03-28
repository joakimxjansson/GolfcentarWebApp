using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class checkoutexitModel : PageModel
    {
        public readonly GolfContext _context;
        private readonly UserService _userService;

        public string Username { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public checkoutexitModel(GolfContext db, UserService userService)
        {
            _context = db;
            _userService = userService;
        }

        public void OnGet(int orderNumber, bool v)
        {
            

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                UserId = int.Parse(userIdClaim);
                Username = _userService.GetUsername(UserId);
            }

            var order = _context.Order
                .AsNoTracking()
                .FirstOrDefault(o => v && o.User.UserId == UserId);
                 OrderDate = DateTime.Now;
                

            if (order != null)
            {
                OrderNumber = int.Parse(order.OrderNumber);
                
            }
        }
    }
}
