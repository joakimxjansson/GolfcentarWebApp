using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;

namespace WebApplication4.Pages
{
    public class Index1Model : PageModel
    {

        //Lista f�r varor i varukorgen
        public List<CartItems> _CartItems { get; set; } = new List<CartItems>();

        public void OnGet()
        {
            // data f�r mallsyfte. H�mtas sen fr�n databas.
            _CartItems = new List<CartItems>
            {
                    new CartItems { ProdId  = new Product(), Quantity = 1 },
                    new CartItems { ProdId = new Product(), Quantity = 1 },
                    new CartItems { ProdId = new Product(), Quantity = 1, },
                    new CartItems { ProdId = new Product(), Quantity = 3, }
            };
        }
    }
}
