using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;

namespace WebApplication4.Pages {
    public class EditProfileModel : PageModel {
        private readonly GolfContext _db;

        public EditProfileModel(GolfContext db) {
            _db = db;
        }

        [BindProperty] public User User { get; set; }

        [BindProperty] public IFormFile UserImage { get; set; }

        public IActionResult OnGet() {
            int? userId = HttpContext.Session.GetInt32("Id");
            if (userId == null) {
                return RedirectToPage("/Login");
            }

            User = _db.Users.Find(userId);

            if (User == null) {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            int? userId = HttpContext.Session.GetInt32("Id");
            if (userId == null) {
                return RedirectToPage("/Login");
            }

            var userToUpdate = await _db.Users.FindAsync(userId);
            if (userToUpdate == null) {
                return RedirectToPage("/Login");
            }

            bool usernameExists = _db.Users.Any(u => u.Username == User.Username && u.UserId != userId);
            if (usernameExists) {
                ModelState.AddModelError(string.Empty, "Anv�ndarnamn �r redan upptaget.");
                return Page();
            }

            bool emailExists = _db.Users.Any(u => u.Email == User.Email && u.UserId != userId);
            if (emailExists) {
                ModelState.AddModelError(string.Empty, "E-postadressen anv�nds redan.");
                return Page();
            }

            //�ndrar & uppdaterar anv�ndarens info
            userToUpdate.FirstName = User.FirstName;
            userToUpdate.LastName = User.LastName;
            userToUpdate.Username = User.Username;
            userToUpdate.Email = User.Email;


            if (UserImage != null) {
                //ta bort gammal bild fr�n wwwroot ifall den inte anv�nds l�ngre
                if (!string.IsNullOrEmpty(userToUpdate.UserImage) &&
                    userToUpdate.UserImage != "/images/DefaultImage.png") {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",
                        Path.GetFileName(userToUpdate.UserImage));
                    if (System.IO.File.Exists(oldImagePath)) {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(UserImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await UserImage.CopyToAsync(stream);
                }

                userToUpdate.UserImage = "/images/" + fileName; //�ndrar & uppdaterar bilden
            } else {
                userToUpdate.UserImage = userToUpdate.UserImage;
            }

            _db.SaveChanges();
            return RedirectToPage();
        }
    }
}