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

        //Lista f�r varor i CartItems
        public List<CartItems> CartItems { get; set; } = new List<CartItems>();

        public checkoutModel(GolfContext db)
        {
            _context = db;
        }

        //H�mta data i CartItems och omvandla till lista
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
        //asynkrod metod f�r att checka ut varor i varukorgen n�r en anv�ndare l�gger en order
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            //H�mtar den inloggade anv�ndarens id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Kollar om anv�ndaren �r inloggad. Om UserId = null �r inte anv�ndaren beh�rig 
            if (userId == null)
            {
                return Unauthorized();
            }
            //H�mtar anv�ndare fr�n databasen med specifikt id
            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }
            //Skapar en ny order och tillderlar den till anv�ndaren (i databasen)
            foreach (var item in CartItems)
            {
                //Skapar en ny order f�r varje produkt i anv�ndarens varukorg
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

        //Metod f�r att visa saldo hon anv�ndare
        public int GetSaldo(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return 0;
            return user.Saldo;
        }
    }
}
