using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.Models.Blog;

namespace WebApplication4.Pages.Blog
{
    public class BlogModel : PageModel
    {
        private readonly GolfContext _context;

        public BlogModel(GolfContext context)
        {
            _context = context;
        }

        public IList<BlogPost> BlogPost { get; set; }

        public async Task OnGetAsync()
        {
            // Hämta alla bloggposter från databasen
            BlogPost = await _context.BlogPosts.ToListAsync();

        }
    }
}
