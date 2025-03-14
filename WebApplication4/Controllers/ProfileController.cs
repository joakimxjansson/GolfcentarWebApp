using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models; 
namespace WebApplication4.Controllers

{

    public class ProfileController : Controller

    {

        public IActionResult EditProfile()

        {


            var userProfile = new UserProfile

            {

                Name = "Rama Alwazir",

                Email = "Rama.Alwazir@example.com",

                PhoneNumber = "1234567890",

                ProfileImage = "/images/default-profile.jpg" 

            };

            return View(userProfile);

        }

        [HttpPost]

        public async Task<IActionResult> EditProfile(UserProfile model, IFormFile profileImage)

        {

            if (!ModelState.IsValid)

            {

                return View(model);

            }

            if (profileImage != null && profileImage.Length > 0)

            {

               

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

         

                using (var stream = new FileStream(filePath, FileMode.Create))

                {

                    await profileImage.CopyToAsync(stream);

                }

             

                model.ProfileImage = "/images/" + fileName;

            }


            ViewData["Message"] = "Profilen har uppdaterats!";

            return View(model);

        }

    }

}