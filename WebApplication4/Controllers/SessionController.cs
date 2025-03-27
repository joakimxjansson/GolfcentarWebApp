using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication4.Controllers
{
    public class SessionController : Controller
    {
        // POST
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  
            return RedirectToPage("/Login"); 
        }
    }
}