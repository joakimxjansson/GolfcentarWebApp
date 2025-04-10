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
        [BindProperty]
        public CartItems CartItem { get; set; }

        public CartService CartService => _cartService;

        public void OnGet()
        {
            CartItems = _cartService.GetCart();
        }

        public IActionResult OnGetRemove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToPage();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            //H�mtar produkten fr�n varukorgen
            var product = _context.Product.FirstOrDefault(p => p.ProductId == id);

            var cartItem = new CartItems
            {
                Product = product, 
                Quantity = 1, 
                TotalPrice = (int)product.ProdPrice 
            };

            _cartService.AddToCart(cartItem);

            //Redirect till DisplayProductTemplate f�r att stanna kvar p� sidan ist�llet f�r att hamna i cart
            return RedirectToPage("/DisplayProductTemplate");
            
        }
    }
}
