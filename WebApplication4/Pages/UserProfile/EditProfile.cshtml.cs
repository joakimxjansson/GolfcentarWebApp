using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication4.Pages.UserProfile
{
    public class EditProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EditProfileModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public User MyUser { get; set; }

        [BindProperty]
        public IFormFile ProfileImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MyUser = await _context.Users.FindAsync(id);
            if (MyUser == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            if (ProfileImage != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                var path = Path.Combine(_env.WebRootPath, "images", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await ProfileImage.CopyToAsync(stream);
                user.UserImage = fileName;
            }

            user.FirstName = MyUser.FirstName;
            user.LastName = MyUser.LastName;
            user.Email = MyUser.Email;
            user.PhoneNumber = MyUser.PhoneNumber;

            await _context.SaveChangesAsync();
            return RedirectToPage("/UserProfile/EditProfile", new { id = user.UserId });
        }
    }
}
