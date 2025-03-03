namespace WebApplication4.Data;

public class SubPost {
    public int SubPostId { get; set; }
    public Post PostId { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
}