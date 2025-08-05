using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqLiteDemo
{
    public class Blog
    {
        public int BlogId { get; set; }
        
        // Fix: Add validation constraints for URL
        [Required(ErrorMessage = "URL is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(500, ErrorMessage = "URL cannot exceed 500 characters")]
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
    }

    public class Post
    {
        public int PostId { get; set; }
        
        public DateTime Created { get; set; }
        
        // Fix: Add validation constraints for Title
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }
        
        // Fix: Add validation constraints for Content
        [Required(ErrorMessage = "Content is required")]
        [MaxLength(5000, ErrorMessage = "Content cannot exceed 5000 characters")]
        public string Content { get; set; }

        // Fix: Add foreign key constraint validation
        [Required]
        public int BlogId { get; set; }
        
        [ForeignKey("BlogId")]
        public Blog Blog { get; set; }
    }
}
