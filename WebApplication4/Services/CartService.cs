using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using WebApplication4.Data;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.EntityFrameworkCore;


namespace WebApplication4.Services
{
    public class CartService
    {
        // Hanterar HTTP-sessionen
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GolfContext _context;
        // Nyckel som används för att spara och hämta varukorgsdata från sessionen
        private const string SessionKey = "Cart";

        

        // Konstruktor
        public CartService(GolfContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public List<CartItems> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartData = session.GetString(SessionKey);

            return cartData == null ? new List<CartItems>() : JsonSerializer.Deserialize<List<CartItems>>(cartData, jsonOptions) ?? new List<CartItems>();
        }

        public void AddToCart(CartItems item)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(p => p.Product.ProductId == item.Product.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }
            SaveCart(cart);
        }

        public void RemoveFromCart(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.CartItemsId == id);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    cart.Remove(item);
                }

                SaveCart(cart);
            }
        }


        public decimal GetTotalPrice()
        {
            return GetCart().Sum(item => item.TotalPrice * item.Quantity);
        }


        public void SaveCart(List<CartItems> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonSerializer.Serialize(cart);
            session.SetString("Cart", cartJson);
        }



        // Sparar varukorgen till databasen som en order
        public void SaveCartToOrder(int userId, string orderNumber)

        {
            var cart = GetCart();
            var user = _context.Users.Find(userId);

            if (user == null)
            {
                throw new InvalidOperationException("Användaren kunde inte hittas");
            }
            Console.WriteLine($"Användar-ID: {userId}");

            foreach (var item in cart)
            {
                var orderDate = DateTime.Now;
                var order = new Order
                {
                    User = user,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Product.ProdPrice,
                    OrderDate = orderDate,
                    OrderNumber = orderNumber,
                };
                _context.Order.Add(order);
                _context.SaveChanges();
            }
            
            ClearCart();
        }
        

        public string GenerateOrderNumber()
        {
            // skapar ett unikt ordernummer med Guid Globally Unique Identifier
            return Guid.NewGuid().ToString();
        }


        //Rensar varukorgen efter att ordern är skapad
        public void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove("Cart");
            Console.WriteLine("Varukorgen har rensats");
        }



    }
}

