using WebApiProject.Dtos.Comment;
using WebApiProject.Models;

namespace WebApiProject.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
    Task<Comment?> DeleteAsync(int id);
}