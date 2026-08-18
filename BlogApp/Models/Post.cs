using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(1000, ErrorMessage = "Content cannot exceed 1000 characters.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [MaxLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
        public string Author { get; set; } = string.Empty;

        [ValidateNever]
        public string? FeatureImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        // M Posts → 1 Category
        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; }

        // 1 Post → Many Comments
        [ValidateNever]
        public ICollection<Comment> Comments { get; set; }
    }
}
