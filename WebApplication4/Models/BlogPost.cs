namespace WebApplication4.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Initialize with a default value
        public string Content { get; set; } = string.Empty; // Initialize with a default value
        public string Author { get; set; } = string.Empty; // Initialize with a default value

        // Lägg till en lista med kommentarer
        public List<Comment> Comments { get; set; } = new List<Comment>(); // Tom lista som standard
    }
}


