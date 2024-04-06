using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.StockDTOs;
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
            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, StockRequestDTO stockDTO)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            if (stockDTO.Symbol != "")
            {
                stockModel.Symbol = stockDTO.Symbol;
            }
            if (stockDTO.CompanyName != "")
            {
                stockModel.CompanyName = stockDTO.CompanyName;
            }
            if (stockDTO.Industry != "")
            {
                stockModel.Industry = stockDTO.Industry;
            }
            if (stockDTO.LastDiv != 0)
            {
                stockModel.LastDiv = stockDTO.LastDiv;
            }
            if (stockDTO.Purchase != 0)
            {
                stockModel.Purchase = stockDTO.Purchase;
            }
            if (stockDTO.MarketCap != 0)
            {
                stockModel.MarketCap = stockDTO.MarketCap;
            }


            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}