using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using WebApplication4.Services;
using System.Text.Json;

namespace WebApplication4.Pages
{
    public class checkoutModel : PageModel
    {
        private readonly GolfContext _context;
        private readonly UserService _userService;

        // Lista för varor i CartItems
        public List<CartItems> CartItems { get; set; } = new List<CartItems>();
        public string Username { get; set; }
        public int UserId { get; set; }
        public int UserSaldo { get; set; }

        public checkoutModel(GolfContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public void OnGet()
        {
            var id = HttpContext.Session.GetInt32("Id");
            Username = _userService.GetUsername(id.Value);
            UserSaldo = _userService.GetSaldo(id.Value);
            // Hämta inloggad användares ID
            //var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            

            // Ladda varukorgens artiklar
            CartItems = _context.CartItems
                .Include(c => c.Product)
                .AsNoTracking()
                .ToList();

            // Beräkna TotalPrice för varje artikel
            foreach (var item in CartItems)
            {
                item.TotalPrice = (int)(item.Product.ProdPrice * item.Quantity);
            }
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            // Hämta användare
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                
                return NotFound("debug");
            }

            // Skapa order för varje produkt
            foreach (var item in CartItems)
            {
                if (item.Product == null)
                {
                    continue; // Skip if product is null
                }

                var order = new Order
                {
                    User = user,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Product.ProdPrice,
                    OrderDate = DateTime.Now,
                    OrderNumber = Guid.NewGuid().ToString()
                };

                _context.Order.Add(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("checkoutexit");
        }

    }
}
