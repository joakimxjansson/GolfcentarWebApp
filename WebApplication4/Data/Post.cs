namespace WebApplication4.Data;

public class Post {
    public int PostId { get; set; }
    public User UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    
}