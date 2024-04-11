using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductDTOs;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateAsync(Product model)
        {
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return null;

            _context.Products.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product?>> GetAllByIdAsync(List<int> ids)
        {
            List<Product?> products = new List<Product?> { };
            foreach (int id in ids)
            {
                products.Append(await _context.Products.FirstOrDefaultAsync(x => x.Id == id));
            }

            return products;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, ProductRequestDTO requestDTO)
        {
            var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return null;


            if (requestDTO.MagnitPrice != 0) model.MagnitPrice = requestDTO.MagnitPrice;
            if (requestDTO.PiaterochkaPrice != 0) model.PiaterochkaPrice = requestDTO.PiaterochkaPrice;
            if (requestDTO.Weight != 0) model.Weight = requestDTO.Weight;
            if (requestDTO.Name != "") model.Name = requestDTO.Name;

            await _context.SaveChangesAsync();

            return model;
        }


    }
}
