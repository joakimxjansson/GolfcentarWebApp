using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;

namespace WebApplication4.Pages.Admin;

public class EditProducts : PageModel
{
    private readonly GolfContext _context;

    public EditProducts(GolfContext context)
    {
        _context = context;
    }
    public IEnumerable<Product>? Products { get; set; } = new List<Product>();
    [BindProperty]
    public Product? Product { get; set; }

    public void OnGet()
    {
        Products = _context.Product;

    }

    //L�gg till produkt
    public IActionResult OnPostCreate()
    {
        if (Product != null)
        {
            _context.Product.Add(Product);
            _context.SaveChanges();
            return RedirectToPage("/Admin/EditProducts");
        }
        return Page();
    }

    //redigera/updatera produkt
    public IActionResult OnPost(int id)
    {
        var product = _context.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        if (Product != null)
        {
            product.ProdName = Product.ProdName;
            product.ProdDescription = Product.ProdDescription;
            product.ProdPrice = Product.ProdPrice;
            product.ProdImage = Product.ProdImage;
            _context.SaveChanges();
            return RedirectToPage("/Admin/EditProducts");
        }
        return Page();
    }

    //Ta bort produkt
    public IActionResult OnPostDelete(int id)
    {
        var product = _context.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Product.Remove(product);
        _context.SaveChanges();
        return RedirectToPage("/Admin/EditProducts");
    }
}
