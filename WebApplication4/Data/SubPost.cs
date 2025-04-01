using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Data;

public class SubPost {
    public int SubPostId { get; set; }
    public Post Post { get; set; }
    [Required]
    public int PostId { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Comment { get; set; }
    public DateTime Date { get; set; }
}