using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class checkoutModel : PageModel
    {
        public readonly GolfContext _context;
        private readonly UserService _userService;

        public List<CartItems> CartItems { get; set; } = new List<CartItems>();
        public string Username { get; set; } 
        public int UserId { get; set; }
        public string OrderNumber { get; set; } 
        public DateTime OrderDate { get; set; }
        public int UserSaldo { get; set; }

        public checkoutModel(GolfContext db, UserService userService)
        {
            _context = db;
            _userService = userService;
        }

        public void OnGet()
        {

            var id = HttpContext.Session.GetInt32("Id");
            //CartItems = _userService.GetCartItems(id.Value);

            UserSaldo = _userService.GetSaldo(id.Value);
            if (id != null)
            {
                UserId = id.Value;
                Username = _userService.GetUsername(UserId);
            }

            CartItems = _context.CartItems
                .Include(c => c.Product)
                .AsNoTracking()
                .ToList();

            foreach (var item in CartItems)
            {
                item.TotalPrice = (int)(item.Quantity * (item.Product.ProdPrice));
            }
        }
        private string GenerateOrderNumber()
        {
            var random = new Random();
            int length = random.Next(5, 8);
            var orderNumber = new char[length];
            for (int i = 0; i < length; i++)
            {
                orderNumber[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(orderNumber);
        }
        async Task<IActionResult> OnPostCheckoutAsync()
        {
            // Kontrollera om användaren är autentiserad
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized();
            }

            // Hämta användarens ID från deras claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Hämta användaren från databasen med hjälp av användarens ID
            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            // Generera ett ordernummer och sätt orderdatumet till nuvarande tid
            var orderNumber = GenerateOrderNumber();
            var orderDate = DateTime.Now;

            // Skapa en order för varje objekt i kundvagnen
            foreach (var item in CartItems)
            {
                var order = new Order
                {
                    User = user,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Product.ProdPrice,
                    OrderDate = orderDate,
                    OrderNumber = orderNumber
                };
                _context.Order.Add(order);
            }

            // Spara ändringarna i databasen
            await _context.SaveChangesAsync();

            // Omdirigera användaren till checkoutexit för orderbekräftelse
            return RedirectToPage("checkoutexit", new { orderNumber = orderNumber, orderDate = orderDate });
        }

        
    }
}
