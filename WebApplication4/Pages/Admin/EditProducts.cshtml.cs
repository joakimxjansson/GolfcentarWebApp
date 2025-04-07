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

    //Lägg till produkt
    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (Product != null)
        {
            if (ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }


                Product.ProdImage = fileName;

            }
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/EditProducts");
        }
        return Page();
    }

    //redigera/updatera produkt
    public async Task<IActionResult> OnPostAsync(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        if (Product != null)
        {
            product.ProdName = Product.ProdName;
            product.ProdDescription = Product.ProdDescription;
            product.ProdPrice = Product.ProdPrice;
            
            if (ImageFile != null)
            {
                //ta bort gammal bild från wwwroot ifall den inte används längre
                if (!string.IsNullOrEmpty(product.ProdImage))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetFileName(product.ProdImage));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // laddar upp ny bild till wwwroot
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                product.ProdImage = "/images/" + fileName;
            }

            else
            {
                product.ProdImage = Product.ProdImage;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/EditProducts");
        }
        return Page();
    }

    //ta bort produkt
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        //ta bort bilden från wwwroot
        if (!string.IsNullOrEmpty(product.ProdImage))
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetFileName(product.ProdImage));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Admin/EditProducts");
    }
}
