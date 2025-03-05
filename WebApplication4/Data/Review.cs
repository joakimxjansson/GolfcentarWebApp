namespace WebApplication4.Data;

public class Review {
    public int ReviewId { get; set; }
    public User UserId { get; set; }
    public Product Product { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}