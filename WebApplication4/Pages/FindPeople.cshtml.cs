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
    

    public FindPeople(GolfContext context, UserService userService) {
        _context = context;
        _userService = userService;
    }
    public void OnGet() {
        Users = _context.Users;


    }

    public IActionResult OnPostSearch() {

        if (Search != null) {
            
        
        var search = _context.Users.Where(u => u.Username.Contains(Search));
        Users = search;
        
        
        
         
       
        
        
        return Page();
        }
        Users = _context.Users;
        return Page();
    }

    public IActionResult OnPostFollow() {
        int? id = HttpContext.Session.GetInt32("Id");
        
        
        var follower = new Follow();
        {
          
                

        };
        _context.Follows.Add(follower);
        _context.SaveChanges();
        Console.WriteLine("Här" + id + " " + UserId );
        return Page();
        
    }
}