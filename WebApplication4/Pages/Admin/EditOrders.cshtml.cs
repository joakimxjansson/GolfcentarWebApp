using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages.Admin;

public class EditOrders : PageModel {
    private readonly GolfContext _context;
    private readonly UserService _userService;
    public EditOrders(GolfContext context ,UserService userService) {
        _context = context;
        _userService = userService;
    }
    public Order? Order { get; set; }
    public IEnumerable<Order>? Orders { get; set; } = new List <Order>();
    public IActionResult OnGet() {
        var id = HttpContext.Session.GetInt32("Id");
         if (id == null){

            return RedirectToPage("/Login");
        }
         
        var role = _userService.GetRole(id.Value);
        if (role == 0) {
            return RedirectToPage("/MyProfile");
           
        }

        Orders = _context.Order
            .Include(o => o.User)
            .Include(o => o.Product)
            .ToList();
        return Page();


    }

    public IActionResult OnPostDelete(int id) {
        var order = _context.Order.Find(id);
        _context.Order.Remove(order);
        _context.SaveChanges();
        return RedirectToPage("/Admin/EditOrders");
        
        
    }



    public IActionResult OnPostDownload() {
        Orders = _context.Order.Include(o => o.User).Include(o => o.Product).ToList();
        return Page();
        
    }
}
