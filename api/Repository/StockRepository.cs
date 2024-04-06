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
    public class StockRepository : IStockRepository
    {
        // не понял на кой надо делать интерфейс, надо покопаться
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<List<Stock>> GetAllStocksAsync()
        {
            return _context.Stocks.ToListAsync();
        }
    }
}