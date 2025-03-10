using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Pages
{

    //hämta produkter från databas och visa på sidan- Anton

    public class DisplayProductTemplateModel : PageModel
    {
       private readonly GolfContext _context;
        public DisplayProductTemplateModel(GolfContext context)
        {
            _context = context;
        }
        
        public List<Product> Product { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
