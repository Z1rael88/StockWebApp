using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Dtos.Stock;
using WebApiProject.helpers;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class StockRepository(ApplicationDbContext dbContext) : IStockRepository
{
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = dbContext.Stocks.Include(c => c.Comments).AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        

        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await dbContext.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await dbContext.Stocks.AddAsync(stock);
        await dbContext.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
        var existingStock = await dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingStock == null)
            return null;
        existingStock.Symbol = stockDto.Symbol;
        existingStock.Industry = stockDto.Industry;
        existingStock.Purchase = stockDto.Purchase;
        existingStock.CompanyName = stockDto.CompanyName;
        existingStock.MarketCap = stockDto.MarketCap;
        existingStock.LastDiv = stockDto.LastDiv;

        await dbContext.SaveChangesAsync();
        return existingStock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await dbContext.Stocks.Include(s=>s.Comments).FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
            return null;

        dbContext.Comments.RemoveRange(stock.Comments);
        dbContext.Stocks.Remove(stock);
        await dbContext.SaveChangesAsync();
        return stock;
    }

    public Task<bool> StockExists(int id)
    {
        return dbContext.Stocks.AnyAsync(s=>s.Id == id);
    }
}