using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using System.Collections.Generic;

namespace WebApplication4.Controllers
{
    public class BlogController : Controller
    {
        private static List<BlogPost> _posts = new List<BlogPost>
        {
            new BlogPost { Id = 1, Title = "Första inlägget", Content = "Det här är ett exempel på ett blogginlägg", Author = "Admin" },
            new BlogPost { Id = 2, Title = "Andra inlägget", Content = "Här är mer innehåll för forumet.", Author = "User1" }
        };

        public IActionResult Index()
        {
            return View(_posts); // Skickar alla blogginlägg till vyn
        }

        public IActionResult Details(int id)
        {
            var post = _posts.Find(p => p.Id == id);
            return View(post); // Skickar det specifika blogginlägget
        }
    }
}
