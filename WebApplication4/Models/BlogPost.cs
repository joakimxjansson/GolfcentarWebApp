namespace WebApplication4.Models.Blog
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }  
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
