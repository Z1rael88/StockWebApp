namespace WebApiProject.Dtos.Comment;

public record CreateCommentRequestDto()
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}