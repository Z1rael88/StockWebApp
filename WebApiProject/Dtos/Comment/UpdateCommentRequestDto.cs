using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(5,ErrorMessage = "Title must be 5 characters")]
    [MaxLength(20,ErrorMessage = "Title cannot be over 20 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5,ErrorMessage = "Content must be 5 characters")]
    [MaxLength(280,ErrorMessage = "Content cannot be over 280 characters")]
    public string Content { get; set; } = string.Empty;
}