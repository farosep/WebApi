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
    public class ProductListRepository : IProductListRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductListRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<List<ProductList>> GetAllProductListsAsync()
        {
            return _context.ProductLists.ToListAsync();
        }
    }
}