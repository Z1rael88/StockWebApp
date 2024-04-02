using Microsoft.AspNetCore.Mvc;
using WebApiProject.Data;
using WebApiProject.Dtos.Stock;
using WebApiProject.Mappers;

namespace WebApiProject.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = dbContext.Stocks.ToList().Select(s => s.ToStockDto());
        return Ok(stocks);
    }
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = dbContext.Stocks.Find(id);

        if (stock == null)
            return NotFound();
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStackFromCreateDto();
        dbContext.Stocks.Add(stockModel);
        dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());

    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = dbContext.Stocks.FirstOrDefault(x => x.Id == id);
        if (stockModel == null)
            return NotFound();

        stockModel.Symbol = updateDto.Symbol;
        stockModel.Industry = updateDto.Industry;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.MarketCap = updateDto.MarketCap;
        stockModel.LastDiv = updateDto.LastDiv;

        dbContext.SaveChanges();
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stock = dbContext.Stocks.FirstOrDefault(x => x.Id == id);

        if (stock == null)
            return NotFound();
        
        dbContext.Stocks.Remove(stock);
        dbContext.SaveChanges();
        return Ok();
    }
}