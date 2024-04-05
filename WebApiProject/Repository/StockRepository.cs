using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Dtos.Stock;
using WebApiProject.Interfaces;
using WebApiProject.Models;

namespace WebApiProject.Repository;

public class StockRepository(ApplicationDbContext dbContext) : IStockRepository
{
    public async Task<List<Stock>> GetAllAsync()
    {
        return  await dbContext.Stocks.Include(c=>c.Comments.ToList()).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await dbContext.Stocks.Include(c=>c.Comments.ToList()).FirstOrDefaultAsync(i=>i.Id == id);
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
        var stock = await dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
            return null;

        dbContext.Stocks.Remove(stock);
        await dbContext.SaveChangesAsync();
        return stock;
    }

    public Task<bool> StockExists(int id)
    {
        return dbContext.Stocks.AnyAsync(s=>s.Id == id);
    }
}