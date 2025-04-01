using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
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

        public List<Post> GetPosts { get; set; } = new List<Post>();

        //hämtar blogginlägg från databasen
        public async Task<IActionResult> OnGetAsync()
        {
            GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate)
            .Include(p => p.User).ToListAsync();
            return Page();
        }

        

        public IActionResult OnPostMyFeed()
        {
            return RedirectToPage("/Feed/MyFeed");
        }

        public IActionResult OnPostFollowing()
        {
            return RedirectToPage("/Feed/Following");
        }
    }
}
