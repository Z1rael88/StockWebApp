using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Dtos.Comment;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class CommentRepository(ApplicationDbContext dbContext) : ICommentRepository
{
    public async Task<List<Comment>> GetAllAsync()
    {
        return await dbContext.Comments.ToListAsync();
        
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await dbContext.Comments.FirstOrDefaultAsync(i=>i.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await dbContext.Comments.AddAsync(comment);
        await dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto)
    {
        var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            return null;

        comment.Title = commentDto.Title;
        comment.Content = commentDto.Content;

        await dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment == null)
            return null;
        
        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
        return comment;
    }
}