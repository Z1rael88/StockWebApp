using Microsoft.AspNetCore.Mvc;
using WebApiProject.Dtos.Comment;
using WebApiProject.Interfaces;
using WebApiProject.Mappers;

namespace WebApiProject.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController(ICommentRepository commentRepository,IStockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await commentRepository.GetAllAsync();
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(comments);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null)
            return NotFound();
        
        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto ,[FromRoute] int stockId)
    {
        if (!await stockRepository.StockExists(stockId))
            return BadRequest("Stock does not exist");

        var commentModel = commentDto.ToCommentFromCreate(stockId);

        await commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id },commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
    {
        var commentModel = await commentRepository.UpdateAsync(id, updateDto);
        if (commentModel == null)
            return NotFound();

        return Ok(commentModel.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await commentRepository.DeleteAsync(id);

        if (stock == null)
            return NotFound();
        
        return Ok();
    }
}