using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages.Feed
{
    public class MyFeedModel : PageModel
    {
        private readonly GolfContext _context;
        
        public MyFeedModel(GolfContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Post> GetPosts { get; set; } = new List<Post>();
        [BindProperty]
        public List<Review> GetReviews { get; set; } = new List<Review>();
        [BindProperty]
        public List<Comment> GetComments { get; set; } = new List<Comment>();

        //hämtar blogginlägg, reviews och comments från databasen
        public async Task<IActionResult> OnGetAsync()
        {
            GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate)
            .Include(p => p.User).ToListAsync();
            
            GetReviews = await _context.Review.OrderByDescending(r => r.Date)
            .Include(r => r.User).Include(r => r.Product).ToListAsync();

            GetComments = await _context.Comments.OrderByDescending(c => c.CreatedAt)
               .Include(c => c.User).ToListAsync();
            return Page();
        }

        //Lägg till kommentar
        public async Task<IActionResult> OnPostAddCommentAsync(int postId, int reviewId, string commentContent)
        {
            int? userId = HttpContext.Session.GetInt32("Id");

            if (userId == null)
            {
                ModelState.AddModelError(string.Empty, "Logga in för att kommentera.");
                return RedirectToPage("/login");
            }

            var comment = new Comment
            {
                PostId = postId,
                ReviewId = reviewId,
                Content = commentContent,
                UserId = (int)userId,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public IActionResult OnPostMyFeed()
        {
            return RedirectToPage("/Feed/MyFeed");
        }

        public IActionResult OnPostFollowing() {
            GetReviews = _context.Review.Where(r => _context.Follows
                    .Any(f => f.FollowerId == HttpContext.Session.GetInt32("Id") && f.FolloweeId == r.UserId))
                .Include(r => r.User)
                .Include(r => r.Product).OrderByDescending(r => r.Date).ToList();
            
            GetComments =  _context.Comments.OrderByDescending(c => c.CreatedAt)
                .Include(c => c.User).OrderByDescending(c => c.CreatedAt).ToList();
            
            GetPosts = _context.Post.Where(p => _context.Follows
                    .Any(f => f.FollowerId == HttpContext.Session.GetInt32("Id") && f.FolloweeId == p.UserId))
                .Include(p => p.User).OrderByDescending(p => p.PublishDate).ToList();
            return Page();
        }

        public IActionResult OnPostFindPeople()
        {
            return RedirectToPage("/FindPeople");
        }
    }
}
