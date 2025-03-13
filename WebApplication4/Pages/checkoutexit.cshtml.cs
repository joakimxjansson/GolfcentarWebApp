using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Pages
{
    public class checkoutexitModel(GolfContext contexts) : PageModel
    {
        private readonly GolfContext _contexts = contexts;
        //Hämta ordernummer
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string order { get; set; }

        public void OnGet()
        {
            var newOrder = new Order
            {
                
                OrderNumber = OrderNumber,
                OrderDate = DateTime.Now
                
            };


            _contexts.Order.Add(newOrder);
            //Behövs för att spara ändringar i databasen? _contexts.SaveChanges();

        }
        public int GetSaldo(int id)
        {
            var user = _contexts.Users.Find(id);
            return user.Saldo;

        }

    }

}
