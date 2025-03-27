using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using System.Collections.Generic;

namespace WebApplication4.Controllers
{
    public class CommentController : Controller
    {
        private static List<Comment> _comments = new List<Comment>();

        public IActionResult Create(int postId)
        {
            var comment = new Comment { PostId = postId };
            return View(comment); // För att skapa en kommentar på ett blogginlägg
        }

        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _comments.Add(comment); // Lägg till kommentar
                return RedirectToAction("Details", "Blog", new { id = comment.PostId });
            }
            return View(comment);
        }
    }
}
