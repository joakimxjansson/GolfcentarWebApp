using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages.Admin;

public class Adminpage : PageModel {
    
    private readonly GolfContext _db;
    private readonly UserService _userService;
    public string Message { get; set; }
    public string Username { get; set; }

    public Adminpage(GolfContext db, UserService userService) {
        _db = db;
        _userService = userService;
    }
    public IActionResult OnGet() {
       
        var id = HttpContext.Session.GetInt32("Id");
        if (id == null) {
            return RedirectToPage("/Login");
        }

        var role = _userService.GetRole(id.Value);
        if (role == 0) {
           return RedirectToPage("/MyProfile");
           
        }
        Username = _userService.GetUsername(id.Value);
        Message = "Välkommen " + Username + "!";
return Page();
    }

    public IActionResult OnPostEditProducts() {
        return RedirectToPage("/Admin/EditProducts");
    }
    public IActionResult OnPostEditUsers() {
        return RedirectToPage("/Admin/EditCustomers");
    }
    
    public IActionResult OnPostEditOrders() {
        return RedirectToPage("/Admin/EditOrders");
    }
}