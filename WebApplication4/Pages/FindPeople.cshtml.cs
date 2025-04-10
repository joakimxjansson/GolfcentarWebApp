using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages;

public class FindPeople : PageModel {
    private readonly GolfContext _context;
    private readonly UserService _userService;
    public IEnumerable<User>? Users { get; set; } = new List<User>();
    [BindProperty]
    public User? User { get; set; }
    [BindProperty]
    public string Search { get; set; }
    public int UserId { get; set; }
    public List <Follow>  Follow { get; set; } = new List<Follow>();
    public Follow? Followers { get; set; }
    
    

    public FindPeople(GolfContext context, UserService userService) {
        _context = context;
        _userService = userService;
    }
    public void OnGet() {
        Users = _context.Users;
        Follow = _context.Follows.ToList();


    }

    public IActionResult OnPostSearch() {

        if (Search != null) {
        
        var search = _context.Users.Where(u => u.Username.Contains(Search));
        Users = search;

            var currentUserId = HttpContext.Session.GetInt32("Id");

            if (currentUserId != null)
            {
                Follow = _context.Follows
                    .Where(f => f.FollowerId == currentUserId)
                    .ToList();
            }
            return Page();
        }
        Users = _context.Users;
        return RedirectToPage("/FindPeople");
    }

    public IActionResult OnPostFollow(int followeeid) {

        if (HttpContext.Session.GetInt32("Id") != null) {
            var id = HttpContext.Session.GetInt32("Id").Value;
            var followee = _context.Users.Find(followeeid);
            
            var follower = new Follow();
            {
                follower.FollowerId = id;
                follower.FolloweeId = followee.UserId;
                
            }
            ;
            if (_context.Follows.Any(f => f.FollowerId == id && f.FolloweeId == followee.UserId)) {
                _context.Follows.Remove(follower);
                _context.SaveChanges();
                return RedirectToPage("/FindPeople");
            }

            _context.Follows.Add(follower);
            _context.SaveChanges();
            Console.WriteLine("Här" + id + " " + followee.UserId);
            return RedirectToPage("/FindPeople");
        }
        return RedirectToPage("/Login");
    }

    public IActionResult OnPostMyFeed()
    {
        return RedirectToPage("/Feed/MyFeed");
    }
}