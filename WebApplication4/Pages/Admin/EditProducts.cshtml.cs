using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages.Admin;

public class EditProducts : PageModel
{
    private readonly GolfContext _context;
    private readonly UserService _userService;

    public EditProducts(GolfContext context ,UserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public IEnumerable<Product>? Products { get; set; } = new List<Product>();
    [BindProperty]
    public Product? Product { get; set; }
    [BindProperty]
    public IFormFile ImageFile { get; set; }

    public IActionResult OnGet()
    {
        var id = HttpContext.Session.GetInt32("Id");
        if (id == null) {
            return RedirectToPage("/Login");
        }
        
        var role = _userService.GetRole(id.Value);
        if (role == 0) {
            return RedirectToPage("/MyProfile");
        }
        Products = _context.Product;
        return Page();
        
        
    }

    //L�gg till produkt
    public IActionResult OnPostCreate()
    {
        if (Product != null)
        {
            if (ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyToAsync(stream);
                }


                Product.ProdImage = fileName;

            }
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
            if (ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                     ImageFile.CopyToAsync(stream);
                }


                product.ProdImage = "/images/" + fileName;

            }
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
