using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using WebApplication4.Data;

namespace WebApplication4.Pages
{
    public class ForumModel : PageModel
    {
        private readonly GolfContext _context;

        public ForumModel(GolfContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<Post> GetPosts { get; set; } = new List<Post>();
        [BindProperty]
        public List<Comment> GetComments { get; set; } = new List<Comment>();

        public async Task<IActionResult> OnGetAsync()
        {
            GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate).ToListAsync();
            GetComments = await _context.Comments.OrderByDescending(c => c.CreatedAt)
                .Include (c => c.User).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAddCommentAsync(int postId, string commentContent)
        {
            int? userId = HttpContext.Session.GetInt32("Id");

            if (userId == null)
            {
                ModelState.AddModelError(string.Empty, "Logga in för att kommentera.");
                return RedirectToPage();
            }

            var comment = new Comment
            {
                PostId = postId,
                Content = commentContent,
                UserId = (int)userId,
                CreatedAt = DateTime.Now 
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
    
}
