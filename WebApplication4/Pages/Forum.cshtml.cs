using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<Post> GetPosts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public async Task<IActionResult> OnGetAsync()
        {
            GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate).ToListAsync();
            Comments = await _context.Comments.ToListAsync(); // Anta att du har en Comment-tabell
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