using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Pages
{
    public class checkoutexitModel : PageModel
    {
        private readonly GolfContext _context;

        public checkoutexitModel(GolfContext context)
        {
            _context = context;
        }

        // Lagrar ordernummer/orderdatum
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }

        public void OnGet()
        {
            // H�mta den inloggade anv�ndarens information
            var userName = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == userName);

            if (user != null)
            {
                var newOrder = new Order
                {
                    OrderNumber = GenerateOrderNumber(), // Generera ett unikt ordernummer
                    OrderDate = DateTime.Now,
                    User = user // Tilldela den inloggade anv�ndaren till ordern
                };

                _context.Order.Add(newOrder);
               // _context.SaveChanges(); // spara �ndringarna i databasen

                // Tilldela ordernumret till newOrder
                OrderNumber = newOrder.OrderNumber;
                OrderDate = newOrder.OrderDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                // Om anv�ndaren inte hittas skrivs f�ljande ut:
                OrderNumber = " Anv�ndaren hittades inte";
                OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        //Metod f�r att generera ett ordernummer
        private string GenerateOrderNumber()
        {
            var random = new Random();
            int length = random.Next(5, 8); // Generera ett nummer mellan 5 och 7
            var orderNumber = new char[length];
            for (int i = 0; i < length; i++)
            {
                orderNumber[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(orderNumber);
        }

    }
}

