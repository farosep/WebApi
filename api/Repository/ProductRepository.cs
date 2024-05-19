using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductDTOs;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Migrations;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            if (requestDTO.LentaPrice != 0) model.LentaPrice = requestDTO.LentaPrice;
            if (requestDTO.Weight != 0) model.Weight = requestDTO.Weight;
            if (requestDTO.Name != "") model.Name = requestDTO.Name;

            await _context.SaveChangesAsync();

            return model;
        }


        public async Task<Product?> CreateAndUpdateAsync(ProductDTO productDTO)
        {
            var model = await _context.Products.FirstOrDefaultAsync(
                x =>
                x.Name == productDTO.Name &&
                x.Weight == productDTO.Weight &&
                x.Volume == productDTO.Volume &&
                x.Amount == productDTO.Amount &&
                x.Percent == productDTO.Percent &&
                x.Brand == productDTO.Brand
                );
            if (model == null)
            {
                var p = new Product
                {
                    MagnitPrice = productDTO.MagnitPrice,
                    LentaPrice = productDTO.LentaPrice,
                    Weight = productDTO.Weight,
                    Amount = productDTO.Amount,
                    Brand = productDTO.Brand,
                    Category = productDTO.Category,
                    SubCategory = productDTO.SubCategory,
                    Percent = productDTO.Percent,
                    Volume = productDTO.Volume,
                    Name = productDTO.Name
                };
                await CreateAsync(p);
                return p;
            }
            if (productDTO.MagnitPrice != 0 && model.MagnitPrice != productDTO.MagnitPrice) model.MagnitPrice = productDTO.MagnitPrice;
            if (productDTO.LentaPrice != 0 && model.LentaPrice != productDTO.LentaPrice) model.LentaPrice = productDTO.LentaPrice;

            if (model.Category != productDTO.Category) model.Category = productDTO.Category;
            if (model.SubCategory != productDTO.SubCategory) model.SubCategory = productDTO.SubCategory;
            return model;
        }

        public async Task<ProductDTO> GetInfoFromTextAsync(
                string str, string category, List<string> subCategories, List<string> brands, string shop)
        {
            var productDTO = new ProductDTO() { };

            string rowStr = str;
            if (rowStr.IsLiquid())
            {
                (productDTO.Volume, rowStr) = rowStr.GetLiquid();
            }
            if (rowStr.IsSolid())
            {
                (productDTO.Weight, rowStr) = rowStr.GetWeight();
            }
            if (rowStr.IsAmount())
            {
                (productDTO.Amount, rowStr) = rowStr.GetAmount();
            }
            if (rowStr.IsPercent())
            {
                (productDTO.Percent, rowStr) = rowStr.GetPercent();
            }
            if (shop == "лента") // надо все такие штуки утащить в статик класс
            {
                (productDTO.LentaPrice, rowStr) = str.GetPrice();
            }
            else if (shop == "магнит")
            {
                (productDTO.MagnitPrice, rowStr) = str.GetPrice();
            }

            (productDTO.Brand, rowStr) = rowStr.GetBrand(brands);
            productDTO.Category = category;
            productDTO.SubCategory = rowStr.GetSubCategory(subCategories);
            productDTO.Name = rowStr;

            return productDTO;
        }
    }
}
