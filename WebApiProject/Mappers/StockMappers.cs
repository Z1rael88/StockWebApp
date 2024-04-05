using WebApiProject.Dtos.Stock;
using WebApiProject.Models;

namespace WebApiProject.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id=stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            Purchase = stockModel.Purchase,
            MarketCap = stockModel.MarketCap,
            Comments = stockModel.Comments.Select(c=>c.ToCommentDto()).ToList()
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
    {
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
}