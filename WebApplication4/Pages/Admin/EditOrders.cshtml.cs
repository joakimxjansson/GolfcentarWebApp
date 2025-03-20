using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
namespace WebApplication4.Pages.Admin;

public class EditOrders : PageModel {
    private readonly GolfContext _context;
    public EditOrders(GolfContext context) {
        _context = context;
    }
    public Order? Order { get; set; }
    public IEnumerable<Order>? Orders { get; set; } = new List <Order>();
    public void OnGet() {

        Orders = _context.Order;



    }
}
