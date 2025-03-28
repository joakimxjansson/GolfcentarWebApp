using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Services;
using WebApplication4.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WebApplication4.Pages.Cart
{
    public class CartModel : PageModel
    {

        private readonly GolfContext _context; 
        private readonly CartService _cartService;

        public CartModel(CartService cartService, GolfContext context)

        {
            _cartService = cartService;
            _context = context; 
        }

        public List<CartItems> CartItems { get; set; } = new();
        public decimal TotalPrice { get; set; }
        
        public int UserId => 1;

        public void OnGet()
        {
            CartItems = _cartService.GetCart(UserId);
            TotalPrice = _cartService.GetTotalPrice(UserId);
        }

        public IActionResult OnGetRemove(int? id)
        {
            if (id == null) return Page();

            _cartService.RemoveFromCart(UserId, id.Value);
            return RedirectToPage();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            //Hämtar produkt från varukorgen
            var product = _context.Product.FirstOrDefault(p => p.ProductId == id);

            _cartService.AddToCart(UserId, id);

            //Redirect till DisplayProductTemplate f�r att stanna kvar p� sidan ist�llet f�r att hamna i cart
            return RedirectToPage("/DisplayProductTemplate");
        }
    }
}
