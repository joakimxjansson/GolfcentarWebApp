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
        private readonly CartService _cartService;

        public List<CartItems> CartItems { get; set; } = new List<CartItems>();
        public string Username { get; set; }
        public int UserId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserSaldo { get; set; }

        public checkoutModel(GolfContext db, UserService userService, CartService cartService)
        {
            _context = db;
            _userService = userService;
            _cartService = cartService;
        }

        public void OnGet()
        {
            // Hämta användarens ID från session
            var id = HttpContext.Session.GetInt32("Id");
            if (id != null)
            {
                UserId = id.Value;
                Username = _userService.GetUsername(UserId);
                UserSaldo = _userService.GetSaldo(UserId);
            }

            // Ladda varukorgens innehåll
            CartItems = _cartService.GetCart();

            foreach (var item in CartItems)
            {
                item.TotalPrice = (int)(item.Quantity * (item.Product.ProdPrice));
            }
        }

        //sparar varorna i ordertabell och tömmer varukorgen
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return Unauthorized();
            }

            var orderNumber = _cartService.GenerateOrderNumber();
            _cartService.SaveCartToOrder(userId.Value, orderNumber);

            return RedirectToPage("checkoutexit", new { orderNumber = orderNumber, orderDate = DateTime.Now });
        }


    }
}
