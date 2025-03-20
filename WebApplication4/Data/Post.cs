using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Data;

public class Post
{
    public int PostId { get; set; }
    public User? User { get; set; }
    public int UserId { get; set; }
    [Required(ErrorMessage = "Titel �r obligatorisk")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Inneh�ll �r obligatoriskt")]
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }

}