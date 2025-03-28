using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using WebApplication4.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Services
{
    public class CartService
    {
        private readonly GolfContext _context;

        public CartService(GolfContext context)
        {
            _context = context;
        }

        public List<CartItems> GetCart(int userId) // Metod för att hämta varukorgen för en användare
        {
            return _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToList();
        }

        public void AddToCart(int userId, int productId)
        {
            var product = _context.Product.FirstOrDefault(p => p.ProductId == productId); // Hämtar produkten från databasen
            if (product == null) return;

            var cartItem = _context.CartItems // Kollar om produkten redan finns i varukorgen
                .FirstOrDefault(ci => ci.UserId == userId && ci.Product.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
                cartItem.TotalPrice += product.ProdPrice;
            }
            else
            {
                _context.CartItems.Add(new CartItems
                {
                    UserId = userId,
                    Product = product,
                    Quantity = 1,
                    TotalPrice = product.ProdPrice
                });
            }

            _context.SaveChanges(); // Sparar ändringarna i databasen
        }

        public void RemoveFromCart(int userId, int productId)
        {
            var cartItem = _context.CartItems
                .FirstOrDefault(ci => ci.UserId == userId && ci.Product.ProductId == productId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }

                _context.SaveChanges();
            }
        }

        public decimal GetTotalPrice(int userId)
        {
            return _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Sum(ci => ci.TotalPrice);
        }
    }
}