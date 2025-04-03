using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Pages
{
    public class checkoutexitModel : PageModel
    {
        public readonly GolfContext _context;
        public string OrderNumber { get; set; } = string.Empty; // Required attributet ersatt med en säker initiering.
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }

        public checkoutexitModel(GolfContext db)
        {
            _context = db;
        }
        public void OnGet(string orderNumber, string orderDate)
        {
            // Hämta användarens ID från HTTP-session
            var sessionId = HttpContext.Session.GetInt32("Id");
            if (sessionId != null)
            {
                UserId = sessionId.Value;

                // Hämta användarens namn 
                var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.UserId == UserId);
                if (user != null)
                {
                    Username = user.Username;
                }
            }
            // Hanterar GET-anrop 
            OrderNumber = orderNumber;

            if (DateTime.TryParse(orderDate, out DateTime parsedDate))
            {
                OrderDate = parsedDate;
            }
            else
            {
                OrderDate = DateTime.MinValue;
            }
        }

        public IActionResult OnPost(string orderNumber, string orderDate)
        {
            // Hanterar POST-anrop från formuläret i checkout
            OrderNumber = orderNumber;

            if (DateTime.TryParse(orderDate, out var parsedDate))
            {
                OrderDate = parsedDate;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ogiltigt datumformat.");
                return Page();
            }

            return Page();
        }
    }
}
