using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using System;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Services;

namespace WebApplication4.Pages
{
    public class checkoutexitModel : PageModel
    {
        public readonly GolfContext _context;
        public readonly CartService _cartService;
        public string OrderNumber { get; set; } = string.Empty; // Required attributet ersatt med en s�ker initiering.
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }

        public checkoutexitModel(GolfContext db ,CartService cartService)
        {
            _cartService = cartService;
            _context = db;
        }
        public IActionResult OnGet(string orderNumber, string orderDate)
        {
            if (HttpContext.Session.GetInt32("Id") == null) {
                
                return RedirectToPage("/Login");
            }

            if (orderNumber == null || orderDate == null) {
                return RedirectToPage("/DisplayProductTemplate");
            }


            // H�mta anv�ndarens ID fr�n HTTP-session
            var sessionId = HttpContext.Session.GetInt32("Id");
            if (sessionId != null)
            {
                UserId = sessionId.Value;

                // H�mta anv�ndarens namn 
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
            return Page();
        }

        public IActionResult OnPost(string orderNumber, string orderDate)
        {
            // Hanterar POST-anrop fr�n formul�ret i checkout
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
