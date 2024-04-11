using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Dtos.Stock;
using WebApiProject.helpers;
using WebApiProject.Interfaces;
using WebApiProject.Mappers;

namespace WebApiProject.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext dbContext,IStockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stocks = await stockRepository.GetAllAsync(query);
        
        var stockDto = stocks.Select(s => s.ToStockDto());
        
        return Ok(stocks);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stock = await stockRepository.GetByIdAsync(id);

        if (stock == null)
            return NotFound();
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stockModel = stockDto.ToStockFromCreateDto();
        
        await stockRepository.CreateAsync(stockModel);
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stockModel = await stockRepository.UpdateAsync(id, updateDto);
        
        if (stockModel == null)
            return NotFound();

        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stock = await stockRepository.DeleteAsync(id);

        if (stock == null)
            return NotFound();
        
        return Ok();
    }
}