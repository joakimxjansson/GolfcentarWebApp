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
    public class BloggModel : PageModel
    {
        private readonly GolfContext _context;

        public BloggModel(GolfContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post NewPost { get; set; } = new Post();

        public List<SubPost> Posts { get; set; } = new List<SubPost>();
        public List<Post> GetPosts { get; set; } = new List<Post>();

        public async Task<IActionResult> OnGetAsync()
        {
            Posts = await _context.SubPost.Include(p => p.Post).OrderByDescending(p => p.Date).ToListAsync();

            int? userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate).ToListAsync();
            }
            else
            {
                GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate).Where(x => x.UserId == userId).ToListAsync();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            int? userId = HttpContext.Session.GetInt32("Id");

            if (userId == null)
            {
                ModelState.AddModelError(string.Empty, "Logga in för att skapa inlägg.");
                Posts = await _context.SubPost.Include(p => p.Post).OrderByDescending(p => p.Date).ToListAsync();
                GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate).ToListAsync();
                return Page();
            }
            NewPost.UserId = (int)userId;
            if (!ModelState.IsValid)
            {
                Posts = await _context.SubPost.Include(p => p.Post).OrderByDescending(p => p.Date).ToListAsync();
                GetPosts = await _context.Post.OrderByDescending(p => p.PublishDate).ToListAsync();
                return Page();
            }

            NewPost.PublishDate = DateTime.Now;

            _context.Post.Add(NewPost);
            await _context.SaveChangesAsync();


            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Blogg/Blogg");
        }
    }
}
