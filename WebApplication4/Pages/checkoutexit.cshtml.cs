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

        public void OnGet(int orderNumber, bool i)
        {
            // Hämtar användarens ID från deras claims (autentiseringsinformation)
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                // Om användarens ID finns, konverterar det till en int och hämtar användarnamnet
                UserId = int.Parse(userIdClaim);
                Username = _userService.GetUsername(UserId);
            }

            // Hämtar en order från databasen som matchar användarens ID och det boolska värdet 'i'
            var order = _context.Order
                .AsNoTracking()
                .FirstOrDefault(o => i && o.User.UserId == UserId);

            // Sätter orderdatumet till nuvarande tid
            OrderDate = DateTime.Now;

            // Om en order hittas, sätter OrderNumber till orderns nummer
            if (order != null)
            {
                OrderNumber = int.Parse(order.OrderNumber);
            }
        }
    }
}
