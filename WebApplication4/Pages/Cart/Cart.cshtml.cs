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
        //Lagrar en instans av CartService
        private readonly CartService _cartService;

        // Konstruktor som injicerar CartService
        public CartModel(CartService cartService)
        {
            _cartService = cartService;
        }

        public List<CartItems> CartItems { get; set; } = new();

        public void OnGet()
        {
            CartItems = _cartService.GetCart();
        }

        public IActionResult OnGetRemove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToPage();
        }
    }
}
