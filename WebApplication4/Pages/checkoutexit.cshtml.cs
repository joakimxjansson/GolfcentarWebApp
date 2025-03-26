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
            // H�mtar anv�ndarens ID fr�n deras claims (autentiseringsinformation)
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                // Om anv�ndarens ID finns, konverterar det till en int och h�mtar anv�ndarnamnet
                UserId = int.Parse(userIdClaim);
                Username = _userService.GetUsername(UserId);
            }

            // H�mtar en order fr�n databasen som matchar anv�ndarens ID och det boolska v�rdet 'i'
            var order = _context.Order
                .AsNoTracking()
                .FirstOrDefault(o => i && o.User.UserId == UserId);

            // S�tter orderdatumet till nuvarande tid
            OrderDate = DateTime.Now;

            // Om en order hittas, s�tter OrderNumber till orderns nummer
            if (order != null)
            {
                OrderNumber = int.Parse(order.OrderNumber);
            }
        }
    }
}
