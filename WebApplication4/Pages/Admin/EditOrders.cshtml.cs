using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
            .Where(o => o.OrderNumber == id).FirstOrDefault();
        using (PdfDocument document = new PdfDocument()) {
            document.PageLayout = PdfPageLayout.SinglePage;
            document.Info.Title = "Kvitto: " + order.OrderNumber;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 12);
            gfx.DrawString("Kvitto från Golfcentar AB", font, XBrushes.Black, new XPoint(200, 20 ));

            gfx.DrawString("Köpare: " + order.User.FirstName + " " + order.User.LastName + " Totalpris: " + order.TotalPrice.ToString() +
               " Produkt(er): " + order.Product.ProdName, font, XBrushes.Black, new XPoint(40, 40 ));
            gfx.DrawString("Köpare: " + order.User.FirstName + " " + order.User.LastName + " Totalpris: " + order.TotalPrice.ToString() +
                         " Produkt(er): " + order.Product.ProdName, font, XBrushes.Black, new XPoint(40, 60 ));
           
            using (MemoryStream ms = new MemoryStream()) {
                document.Save(ms);
                return File(ms.ToArray(), "application/pdf");
            }
        }
        Orders = _context.Order.Include(o => o.User).Include(o => o.Product).ToList();
        return Page();
        
    }
}
