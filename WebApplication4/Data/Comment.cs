using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Data {
    public class Comment {
        public int? CommentID { get; set; }
        public User? User { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? PostId { get; set; }

        [Required(ErrorMessage = "Du måste logga in för att kommentera")]
        public int? UserId { get; set; }

        public int? ReviewId { get; set; }
    }
}