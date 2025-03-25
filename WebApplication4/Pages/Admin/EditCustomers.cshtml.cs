using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages.Admin;

public class EditCustomers : PageModel {
    private readonly GolfContext _context;
    private readonly UserService _userService;

    public EditCustomers(GolfContext context ,UserService userService) {
        _context = context;
        _userService = userService;
    }
    public IEnumerable<User>? Users { get; set; } = new List<User>();
    [BindProperty]
    public User? User { get; set; } 
    [BindProperty]
    public IFormFile ImageFile { get; set; } 
    
   
    
    public IActionResult OnGet() {
        
        var id = HttpContext.Session.GetInt32("Id");
        if (id == null) {
            return RedirectToPage("/Login");
        }
        var role = _userService.GetRole(id.Value);
        if (role == 0) {
            return RedirectToPage("/MyProfile");
        }
        Users = _context.Users;
        return Page();

    }

    public async Task <IActionResult> OnPostUpdateAsync(int id) {
        
        
        var user = await _context.Users.FindAsync(id);
    

        if (user == null) {
            return NotFound();
        }

        if (User != null) {
            user.FirstName = User.FirstName;
            user.LastName = User.LastName;
            user.Email = User.Email;
            user.Saldo = User.Saldo;
            if (ImageFile != null) {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

           
            user.UserImage = "/images/" + fileName;
        
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/EditCustomers");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) {
            return NotFound();
        }
        

        if (user.Admin != 0) {
            return RedirectToPage("/Admin/EditCustomers");
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Admin/EditCustomers");
    }
}
