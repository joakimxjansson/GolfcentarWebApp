using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Pages
{
    public class checkoutModel : PageModel
    {
        // Kontext f�r databasen 

        public readonly GolfContext _context;

        public int UserTest { get; set; } //h�mta anv�ndarens id
        //konstrukt�r f�r att skapa en instans av databasen
        public checkoutModel(GolfContext db)
        {
            _context = db;
        }


        //Lista f�r varor i varukorgen
        public List<CartItems> CartItems { get; set; } = new List<CartItems>();
        public int UserId { get; set; }

        // H�mtar alla varukorgens artiklar fr�n databasen och produktdata
        public void OnGet()
        {
            CartItems = _context.CartItems
                .Include(c => c.Product)
                .ToList();         
        }
        //F�r att h�mta anv�ndarens saldo
        public int GetSaldo(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new Exception($"Kunde inte hitta anv�dnare med id: {id}");
            }
            return user.Saldo;
        }



        /*
             CartItems = new List<CartItems>
             {
                 new CartItems { Product = new Product { ProdName = "Product1", ProdPrice = 1000m }, Quantity = 1, TotalPrice = 1000 },
                 new CartItems { Product = new Product { ProdName = "Vantar ProductID #100", ProdPrice = 200m }, Quantity = 2, TotalPrice = 400 },
                 new CartItems { Product = new Product { ProdName = "M�ssa ProductID #101", ProdPrice = 100m }, Quantity = 1, TotalPrice = 100 },
                 new CartItems { Product = new Product { ProdName = "Driver ProductID #103", ProdPrice = 1345m }, Quantity = 3, TotalPrice = 4035 }
             };
             */
    }
}
