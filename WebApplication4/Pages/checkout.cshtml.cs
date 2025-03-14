using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Pages
{
    public class checkoutModel : PageModel
    {
        // Kontext för databasen 

        public readonly GolfContext _context;

        public int UserTest { get; set; } //hämta användarens id
        //konstruktör för att skapa en instans av databasen
        public checkoutModel(GolfContext db)
        {
            _context = db;
        }


        //Lista för varor i varukorgen
        public List<CartItems> CartItems { get; set; } = new List<CartItems>();
        public int UserId { get; set; }

        // Hämtar alla varukorgens artiklar från databasen och produktdata
        public void OnGet()
        {
            CartItems = _context.CartItems
                .Include(c => c.Product)
                .ToList();         
        }
        //För att hämta användarens saldo
        public int GetSaldo(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new Exception($"Kunde inte hitta anvädnare med id: {id}");
            }
            return user.Saldo;
        }



        /*
             CartItems = new List<CartItems>
             {
                 new CartItems { Product = new Product { ProdName = "Product1", ProdPrice = 1000m }, Quantity = 1, TotalPrice = 1000 },
                 new CartItems { Product = new Product { ProdName = "Vantar ProductID #100", ProdPrice = 200m }, Quantity = 2, TotalPrice = 400 },
                 new CartItems { Product = new Product { ProdName = "Mössa ProductID #101", ProdPrice = 100m }, Quantity = 1, TotalPrice = 100 },
                 new CartItems { Product = new Product { ProdName = "Driver ProductID #103", ProdPrice = 1345m }, Quantity = 3, TotalPrice = 4035 }
             };
             */
    }
}
