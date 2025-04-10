using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;

namespace WebApplication4.Pages.Blogg {
    public class EditBloggModel : PageModel {
        private readonly GolfContext _context;

        public EditBloggModel(GolfContext context) {
            _context = context;
        }

        [BindProperty] public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int id) {
            if (id == 0) {
                return NotFound();
            }

            Post = await _context.Post.FindAsync(id);

            if (Post == null) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            if (Post.PostId == 0) {
                return RedirectToPage("/Blogg/EditBlogg");
            } else {
                Post.PublishDate = System.DateTime.Now;
                _context.Attach(Post).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Blogg/Blogg");
        }
    }
}