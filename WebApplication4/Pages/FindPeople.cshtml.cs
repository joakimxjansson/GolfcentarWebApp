using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Pages;

public class FindPeople : PageModel {
    private readonly GolfContext _context;
    private readonly UserService _userService;
    public IEnumerable<User>? Users { get; set; } = new List<User>();
    [BindProperty] public User? User { get; set; }
    [BindProperty] public string Search { get; set; }
    public int UserId { get; set; }
    public List<Follow> Follow { get; set; } = new List<Follow>();
    public Follow? Followers { get; set; }


    public FindPeople(GolfContext context, UserService userService) {
        _context = context;
        _userService = userService;
    }

    public void OnGet() {
        Users = _context.Users;
        Follow = _context.Follows.ToList();
    }

    public async Task<IActionResult> OnPostSearchAsync() {
        var currentUserId = HttpContext.Session.GetInt32("Id");

        if (!string.IsNullOrWhiteSpace(Search)) {
            Users = await _context.Users
                .Where(u => u.Username.Contains(Search))
                .ToListAsync();

            if (currentUserId != null) {
                Follow = await _context.Follows
                    .Where(f => f.FollowerId == currentUserId)
                    .ToListAsync();
            }

            return Page();
        }

        Users = await _context.Users.ToListAsync();
        return RedirectToPage("/FindPeople");
    }

    public async Task<IActionResult> OnPostFollowAsync(int followeeid) {
        if (HttpContext.Session.GetInt32("Id") != null) {
            var id = HttpContext.Session.GetInt32("Id").Value;
            var followee = await _context.Users.FindAsync(followeeid);

            var follower = new Follow {
                FollowerId = id,
                FolloweeId = followee.UserId
            };

            if (await _context.Follows.AnyAsync(f => f.FollowerId == id && f.FolloweeId == followee.UserId)) {
                _context.Follows.Remove(follower);
                await _context.SaveChangesAsync();
                return RedirectToPage("/FindPeople");
            }

            _context.Follows.Add(follower);
            await _context.SaveChangesAsync();
            return RedirectToPage("/FindPeople");
        }

        return RedirectToPage("/Login");
    }

    public IActionResult OnPostMyFeed() {
        return RedirectToPage("/Feed/MyFeed");
    }
}