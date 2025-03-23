using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4;
using WebApplication4.Data;
using System.Linq;

namespace WebApplication4.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly GolfContext _db;

        public ProductDetailsModel(GolfContext db)
        {
            _db = db;
        }

        public Product Product { get; set; }
        public IActionResult OnGet(int id)
        {
            Product = _db.Product.FirstOrDefault(p => p.ProductId == id); //hämta produkt med id fr. databas

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
