using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductDTOs;
using api.Extensions;
using api.Helpers;
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

        public async Task<Product?> DeleteAsync(int id, AppUser appUser)
        {
            var model = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return null;

            _context.Products.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<Product>> GetAllAsync(AppUser appUser, QueryObject query)
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(AppUser appUser, int id)
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

        public async Task<Product?> UpdateAsync(AppUser appUser, int id, ProductRequestDTO requestDTO)
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

        public async Task<Product?> CreateAndUpdateAsync(string name, int? volume, float? percent, string? category, int? amount, int? weight, float? price)
        {
            var model = await _context.Products.FirstOrDefaultAsync(x => x.Name == name);
            if (model == null)
            {
                var p = new Product
                {
                    MagnitPrice = price,
                    Weight = weight,
                    Amount = amount,
                    Category = category,
                    Percent = percent,
                    Volume = volume,
                    Name = name
                };
                await CreateAsync(p);
                return p;
            }
            if (model.MagnitPrice != price) model.MagnitPrice = price;
            if (model.Weight != weight) model.Weight = weight;
            if (model.Amount != amount) model.Amount = amount;
            if (model.Category != category) model.Category = category;
            if (model.Percent != percent) model.Percent = percent;
            if (model.Volume != volume) model.Volume = volume;
            return model;
        }

        public async Task<(string, int?, float?, string?, int?, int?, float?)> GetInfoFromTextAsync(string str)
        {
            int? weight = 0;
            int? volume = 0;
            int? amount = 0;
            string? category = "";
            float? percent = 0;

            var (price, name) = str.GetPrice();
            // надо отрезать нужный текст и возвращать строку без части текста ( цена, объём и т.д)
            if (name.IsLiquid())
            {
                (volume, name) = name.GetLiquid();
            }
            else if (name.IsSolid())
            {
                (weight, name) = name.GetWeight();
            }
            else if (name.IsAmount())
            {
                (amount, name) = name.GetAmount();
            }
            if (name.IsPercent())
            {
                (percent, name) = name.GetPercent();
            }
            (category, name) = name.GetCategory();

            return (name, volume, percent, category, amount, weight, price);
        }
    }
}
