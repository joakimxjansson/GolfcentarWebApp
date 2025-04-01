using System;

namespace WebApplication4.Data

{
    public class Comment
    {
        public int? CommentID { get; set; }
        public User? User { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public int? ReviewId { get; set; }
    }
}