using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models.Blog;

namespace WebApplication4.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly GolfContext _context;

        public DetailsModel(GolfContext context)
        {
            _context = context;
        }

        public BlogPost BlogPost { get; set; }

        [BindProperty]
        public string CommentContent { get; set; }

        public async Task OnGetAsync(int id)
        {
            BlogPost = await _context.BlogPosts
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(m => m.BlogPostId == id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid)
            {
                var blogPost = await _context.BlogPosts.FindAsync(id);

                var comment = new Comment
                {
                    Content = CommentContent,
                    CreatedAt = DateTime.Now,
                    Author = "Anonym", // Här kan du sätta användarens namn om inloggning finns
                    BlogPostId = id
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Details", new { id = blogPost.BlogPostId });
            }
            return Page();
        }
    }
}
