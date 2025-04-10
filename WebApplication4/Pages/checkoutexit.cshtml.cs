using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using System;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Services;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.UniversalAccessibility.Drawing;

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

        public IActionResult OnPostDownload(string id) {
             var order = _context.Order.Include(p => p.User)
            .Include(o => o.Product)
            .Where(o => o.OrderNumber == id).ToList();
        
        using (PdfDocument document = new PdfDocument()) {
            document.PageLayout = PdfPageLayout.SinglePage;
            document.Info.Title = "Kvitto: " + order.First().OrderNumber;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 12);
            int y = 180;
            
            
            gfx.DrawString("Kvitto från Golfcentar AB", font, XBrushes.Black, new XPoint(200, 20 ));
            
            gfx.DrawString("Kund: " + order.First().User.FirstName + " " + order.First().User.LastName, font, XBrushes.Black, new XPoint(70, 60 ) );
            gfx.DrawString("E-post: " + order.First().User.Email, font, XBrushes.Black, new XPoint(70, 80 ) );
            gfx.DrawString("Ordernummer: " + order.First().OrderNumber, font, XBrushes.Black, new XPoint(70, 100 ) );
            gfx.DrawString("Datum för köp: " + order.First().OrderDate, font, XBrushes.Black, new XPoint(70, 120 ) );

            gfx.DrawString("Produkt(er): ", font, XBrushes.Black, new XPoint(70, 160 ) );
            foreach (var orders in order) {
                gfx.DrawString(orders.Product.ProdName + " Pris: " + orders.Product.ProdPrice + ":- inkl moms  (" + orders.Product.ProdPrice/1.25m + ":- exkl moms)", font, XBrushes.Black, new XPoint(100, y));
                 y += 20;
            }

            double center = page.Width / 2;
            gfx.DrawString("Totalpris: " + order.First().TotalPrice + ":- inkl moms  (" + order.First().TotalPrice/1.25m + ":- exkl moms)" , 
                font, XBrushes.Black, new XPoint(70, y+20 ) );
            
            gfx.DrawString("Tack för att du handlat på Centarshoppen. Välkommen åter!" , 
                font, XBrushes.Black, new XPoint(100, y+50 ) );

            using (MemoryStream ms = new MemoryStream()) {
                document.Save(ms);
                return File(ms.ToArray(), "application/pdf");
            }
        }
       
        }
    }
}
