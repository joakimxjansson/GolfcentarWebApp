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
            // h�rdkodad produktf�rslag till mallen
            Product = new Product
            {
                ProductId = 1,
                ProdName = "Golfklubba Driver",
                ProdDescription = "Fin driver av h�gsta kvalit�",
                ProdPrice = 2599,
                ProdImage = "driver.jpg"
            };
        }
    }
}
