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

        public async Task<ProductList> CreateAsync(ProductList model)
        {
            await _context.ProductLists.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ProductList?> DeleteAsync(int id)
        {
            var model = await _context.ProductLists.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return null;

            _context.ProductLists.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<ProductList>> GetAllAsync()
        {
            return await _context.ProductLists.Include(
                c => c.Products).ToListAsync();
        }

        public async Task<ProductList?> GetByIdAsync(int id)
        {
            return await _context.ProductLists.Include(
                c => c.Products).FirstOrDefaultAsync(
                    i => i.Id == id);
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductList?> UpdateAsync(int id, ProductListRequestDTO requestDTO)
        {
            var model = await _context.ProductLists.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return null;

            if (requestDTO.Products != new List<Product>()) model.Products = requestDTO.Products;

            if (requestDTO.Name != "") model.Name = requestDTO.Name;

            if (requestDTO.UserId != 0) model.UserId = requestDTO.UserId;


            await _context.SaveChangesAsync();

            return model;
        }
    }
}