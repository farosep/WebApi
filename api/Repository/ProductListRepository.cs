using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductListDTOs;
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

        public Task<ProductList> CreateAsync(ProductList protoModel)
        {
            throw new NotImplementedException();
        }

        public Task<ProductList?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductList>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductList>> GetAllProductListsAsync()
        {
            return _context.ProductLists.ToListAsync();
        }

        public Task<ProductList?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductList?> UpdateAsync(int id, ProductListRequestDTO protoRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}