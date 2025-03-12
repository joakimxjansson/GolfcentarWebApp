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

    public IActionResult OnPost(int id) {
        var user = _context.Users.Find(id);
        if (user == null) {
            return NotFound();
        }

        if (User != null) {
            user.FirstName = User.FirstName;
            user.LastName = User.LastName;
            user.Email = User.Email;
            user.Saldo = User.Saldo;
            _context.SaveChanges();
            return RedirectToPage("/Admin/EditCustomers");
        }
        return Page();
    }
}
