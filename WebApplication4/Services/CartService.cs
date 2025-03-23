using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using WebApplication4.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication4.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "Cart";

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
                if(item.Quantity > 1)
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
        
        private void SaveCart(List<CartItems> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(SessionKey, JsonSerializer.Serialize(cart, jsonOptions));
        }
    }
}
