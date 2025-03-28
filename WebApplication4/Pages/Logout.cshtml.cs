using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication4.Pages;

public class Logout : PageModel {
    public IActionResult OnGet() {
        HttpContext.Session.Clear();
        return RedirectToPage("/Login");  
    }
}