using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages.Admin;

public class EditCustomers : PageModel {
    private readonly GolfContext _context;
    private readonly UserService _userService;

    public EditCustomers(GolfContext context, UserService userService) {
        _context = context;
        _userService = userService;
    }

    public IEnumerable<User>? Users { get; set; } = new List<User>();
    [BindProperty] public User? User { get; set; }
    [BindProperty] public IFormFile ImageFile { get; set; }


    public IActionResult OnGet() {
        var id = HttpContext.Session.GetInt32("Id");
        if (id == null) {
            return RedirectToPage("/Login");
        }

        var role = _userService.GetRole(id.Value);
        if (role == 0) {
            return RedirectToPage("/MyProfile");
        }

        Users = _context.Users;
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateAsync(int id) {
        var user = await _context.Users.FindAsync(id);


        if (user == null) {
            return NotFound();
        }

        if (User != null) {
            user.FirstName = User.FirstName;
            user.LastName = User.LastName;
            user.Email = User.Email;
            user.Saldo = User.Saldo;

            if (ImageFile != null) {
                //ta bort gammal bild fr�n wwwroot ifall den inte anv�nds l�ngre f�rutom om det �r defaultimage
                if (!string.IsNullOrEmpty(user.UserImage) && user.UserImage != "/images/DefaultImage.png") {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",
                        Path.GetFileName(user.UserImage));
                    if (System.IO.File.Exists(oldImagePath)) {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                //spara ny bild i wwwroot
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await ImageFile.CopyToAsync(stream);
                }

                user.UserImage = "/images/" + fileName;
            } else {
                user.UserImage = user.UserImage;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/EditCustomers");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) {
            return NotFound();
        }

        //tar bort f�ljar relationer som anv�ndaren f�ljer
        var followerFollows = _context.Follows.Where(f => f.FollowerId == id);
        _context.Follows.RemoveRange(followerFollows);

        //tar bort f�ljar relationer som f�ljer anv�ndaren som tas bort
        var followeeFollows = _context.Follows.Where(f => f.FolloweeId == id);
        _context.Follows.RemoveRange(followeeFollows);

        //radera userimage ur wwwroot f�rutom om det �r defaultimage
        if (!string.IsNullOrEmpty(user.UserImage) && user.UserImage != "/images/DefaultImage.png") {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",
                Path.GetFileName(user.UserImage));
            if (System.IO.File.Exists(imagePath)) {
                System.IO.File.Delete(imagePath);
            }
        }

        if (user.Admin != 0) {
            return RedirectToPage("/Admin/EditCustomers");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Admin/EditCustomers");
    }
}