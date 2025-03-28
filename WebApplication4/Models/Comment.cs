namespace WebApplication4.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Author { get; set; } = string.Empty; // Initialize with a default value
        public string Content { get; set; } = string.Empty; // Initialize with a default value
    }
}
