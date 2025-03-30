using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using WebApplication4.Data;
using System;

namespace WebApplication4.Services
{
    public class CartService
    {
        // Hanterar HTTP-sessionen
        private readonly IHttpContextAccessor _httpContextAccessor;
        // Nyckel som används för att spara och hämta varukorgsdata från sessionen
        private const string SessionKey = "Cart";

        // Konstruktor som tar in IHttpContextAccessor för att få åtkomst till sessionen
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
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
            var existingItem = cart.FirstOrDefault(p => p.CartItemsId == item.CartItemsId);

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

        public int GetQunatity() {
            return GetCart().Sum(p => p.Quantity);
        }

        public void RemoveFromCart(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.CartItemsId == id);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        //Sparar varukorgen i sessionen
        private void SaveCart(List<CartItems> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(SessionKey, JsonSerializer.Serialize(cart, jsonOptions));
        }
    }
}
