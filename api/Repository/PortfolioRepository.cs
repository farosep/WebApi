using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return _context.Portfolios
            .Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.stock.Symbol,
                Name = stock.stock.Name,
                Purchase = stock.stock.Purchase,
                LastDiv = stock.stock.LastDiv,
                Industry = stock.stock.Industry,
                MarketCap = stock.stock.MarketCap
            }).ToListAsync();
        }
    }
}