using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;

namespace WebApplication4.Pages
{
    public class checkoutModel : PageModel
    {
        public readonly GolfContext _context;

        //Lista för varor i CartItems
        public List<CartItems> CartItems { get; set; } = new List<CartItems>();

        public checkoutModel(GolfContext db)
        {
            _context = db;
        }

        //Hämta data i CartItems och omvandla till lista
        public void OnGet()
        {
            CartItems = _context.CartItems
                .Include(c => c.Product)
                .AsNoTracking()
                .ToList();

            foreach (var item in CartItems)
            {
                item.TotalPrice = (int)(item.Quantity * (item.Product.ProdPrice));
            }
        }
        //asynkrod metod för att checka ut varor i varukorgen när en användare lägger en order
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            //Hämtar den inloggade användarens id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Kollar om användaren är inloggad. Om UserId = null är inte användaren behörig 
            if (userId == null)
            {
                return Unauthorized();
            }
            //Hämtar användare från databasen med specifikt id
            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }
            //Skapar en ny order och tillderlar den till användaren (i databasen)
            foreach (var item in CartItems)
            {
                //Skapar en ny order för varje produkt i användarens varukorg
                var order = new Order
                {
                    User = user,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Product.ProdPrice,
                    OrderDate = DateTime.Now,
                    OrderNumber = Guid.NewGuid().ToString()
                };
                //Skapar order i databas
                _context.Order.Add(order);
            }
            //Sparar data och retrunerar till sidan checkoutexit
            await _context.SaveChangesAsync();

            return RedirectToPage("checkoutexit");
        }

        public int UserSaldo { get; set; }

        //Metod för att visa saldo hon användare
        public int GetSaldo(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return 0;
            return user.Saldo;
        }
    }
}
