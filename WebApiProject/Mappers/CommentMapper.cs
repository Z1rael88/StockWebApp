using WebApiProject.Dtos.Comment;
using WebApiProject.Models;

namespace WebApiProject.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn
        };
    }
    public static Comment ToCommentFromCreate(this CreateCommentRequestDto commentDto,int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId
        };
    }
}