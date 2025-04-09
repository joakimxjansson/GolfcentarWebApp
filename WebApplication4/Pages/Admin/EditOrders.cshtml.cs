using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.UniversalAccessibility.Drawing;
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
