using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;

namespace WebApplication4.Pages
{
    public class DisplayProductTemplateModel : PageModel
    {
        public Product Product { get; set; }

        public void OnGet()
        {
            // hårdkodad produktförslag till mallen
            Product = new Product
            {
                ProductId = 1,
                ProdName = "Golfklubba Driver",
                ProdDescription = "Fin driver av högsta kvalité",
                ProdPrice = 2599,
                ProdImage = "driver.jpg"
            };
        }
    }
}
