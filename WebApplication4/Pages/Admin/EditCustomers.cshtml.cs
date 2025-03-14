using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
namespace WebApplication4.Pages.Admin;

public class EditCustomers : PageModel {
    private readonly GolfContext _context;

    public EditCustomers(GolfContext context) {
        _context = context;
    }
    public IEnumerable<User>? Users { get; set; } = new List<User>();
    [BindProperty]
    public User? User { get; set; } 
   
    
    public void OnGet() {
        Users = _context.Users;

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
