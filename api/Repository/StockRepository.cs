
using api.Data;
using api.DTO.StockDTOs;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        // не понял на кой надо делать интерфейс, надо покопаться
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) return null;

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(AppUser appUser, QueryObject query)
        {
            var stocks = _context.Stocks.Include(
                c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                stocks = stocks.Where(s => s.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            // тут сделана сортировка по полю symbol, по хорошему надо бы сделать 
            // отдельный статик класс с нужными переменными
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(
                        s => s.Symbol) : stocks.OrderBy(
                            s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(AppUser appUser, int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Stocks.AnyAsync(i => i.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, StockRequestDTO stockDTO)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) return null;

            if (stockDTO.Symbol != "") stockModel.Symbol = stockDTO.Symbol;

            if (stockDTO.Name != "") stockModel.Name = stockDTO.Name;

            if (stockDTO.Industry != "") stockModel.Industry = stockDTO.Industry;

            if (stockDTO.LastDiv != 0) stockModel.LastDiv = stockDTO.LastDiv;

            if (stockDTO.Purchase != 0) stockModel.Purchase = stockDTO.Purchase;

            if (stockDTO.MarketCap != 0) stockModel.MarketCap = stockDTO.MarketCap;

            // тут есть косяк, если захотим занулить инты то не сможем.

            await _context.SaveChangesAsync();

            return stockModel;
        }

    }
}