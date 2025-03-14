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
        public SubPost NewPost { get; set; } = new SubPost();

        public List<SubPost> Posts { get; set; } = new List<SubPost>();

        public async Task<IActionResult> OnGetAsync()
        {
            Posts = await _context.SubPost.OrderByDescending(p => p.Date).ToListAsync(); 
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Posts = await _context.SubPost.OrderByDescending(p => p.Date).ToListAsync();
                return Page();
            }

            NewPost.Date = DateTime.Now;
            _context.SubPost.Add(NewPost);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
