using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4;
using WebApplication4.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Services;

namespace WebApplication4.Pages {
    public class ProductDetails2Model : PageModel {
        private readonly GolfContext _db;
        private readonly CartService _cartService;

        public ProductDetails2Model(GolfContext db, CartService cartService) {
            _db = db;
            _cartService = cartService;
        }

        [BindProperty] public Review CreateReview { get; set; } = new Review();
        public Product Product { get; set; } = default!;
        public List<Review> Reviews { get; set; } = new(); //list för att visa reviews

        public int? CurrentUserId { get; set; } //för att tracka redan inloggad användare

        public bool AddedCartItem { get; set; } //Om produkt lagts till i kundvagn

        public async Task<IActionResult> OnGetAsync(int id) {
          
            Product = await _db.Product
                .Include(p => p.Reviews) //hämtar reviews relaterad till produkten
                .ThenInclude(r => r.User) //användaren som skrev reviewen
                .FirstOrDefaultAsync(p => p.ProductId == id); //hämta produkt med id fr. databas

            if (Product == null) {
                return NotFound();
            }

            Reviews = Product.Reviews.OrderByDescending(r => r.Date).ToList(); //sorterar reviews efter datum

            int? userId = HttpContext.Session.GetInt32("Id");
            CurrentUserId = userId; // Spara det i Model

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id) {
            int? userId = HttpContext.Session.GetInt32("Id");

            if (userId == null) {
                TempData["ErrorMessage"] = "Du måste vara inloggad för att skriva en recension.";
                return RedirectToPage(new { id = id });
            }

            if (!ModelState.IsValid) {
                return Page();
            }

            CreateReview.ProductId = id;
            CreateReview.UserId = userId.Value;
            CreateReview.Date = DateTime.Now;

            _db.Review.Add(CreateReview);
            await _db.SaveChangesAsync();


            return RedirectToPage(new { id = id });
        }

        //egen OnPostAddToCart för att lägga till i kundvagn på detaljsida utan att skickas tillbaka till template-sidan
        public IActionResult OnPostAddToCart(int id) {
            var product = _db.Product.FirstOrDefault(p => p.ProductId == id); //kod taget fr Cart.cshtml.cs
            if (product == null) {
                return NotFound();
            }

            var cartItem = new CartItems {
                Product = product,
                Quantity = 1,
                TotalPrice = (int)product.ProdPrice
            };

            _cartService.AddToCart(cartItem);

            AddedCartItem = true; //produkt har lagts i kundvagn(true)


            // Hämtar produkten igen så sidan kan laddas om rätt
            Product = _db.Product
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefault(p => p.ProductId == id);

            Reviews = Product.Reviews
                .OrderByDescending(r => r.Date)
                .ToList();

            return Page(); // stannar på samma sida!
        }


        //metod för att kunna radera egna recensioner 
        public async Task<IActionResult> OnPostDeleteReviewAsync(int reviewId, int productId) {
            int? userId = HttpContext.Session.GetInt32("Id");

            if (userId == null) {
                //kräver inlogg
                TempData["ErrorMessage"] = "Du måste vara inloggad för att radera en recension.";
                return RedirectToPage(new { id = productId });
            }

            var review = await _db.Review.FindAsync(reviewId);

            if (review == null || review.UserId != userId.Value) {
                TempData["ErrorMessage"] = "Du kan bara radera dina egna recensioner.";
                return RedirectToPage(new { id = productId });
            }

            _db.Review.Remove(review);
            await _db.SaveChangesAsync();

            return RedirectToPage(new { id = productId });
        }
    }
}