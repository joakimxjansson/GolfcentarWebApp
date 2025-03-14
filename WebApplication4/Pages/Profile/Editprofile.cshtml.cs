
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Pages.Profile
{
    public class Editprofile :PageModel 
      

    {
        public UserProfile User { get; set; } = new UserProfile();



        public void OnGet()

        {

        }

        public IActionResult OnPost()

        {

            if (!ModelState.IsValid)

            {

                return Page();

            }

            // Här kan du spara datan i en databas om du vill

            return RedirectToPage("/Editprofile"); // Ladda om sidan efter sparning

        }



    }
}
