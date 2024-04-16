using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductListDTOs;
using api.Helpers;
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
            var model = await _context.ProductLists.FirstOrDefaultAsync(
                    x => x.Id == id);
            if (model == null) return null;

            _context.ProductLists.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<ProductList>> GetAllAsync(AppUser appUser, QueryObject query)
        {
            var productLists = _context.PLUserModels.Where(u => u.UserId == appUser.Id)
            .Select(pl => new ProductList
            {
                Id = pl.productList.Id,
                Name = pl.productList.Name,
            })
            .AsQueryable();



            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                productLists = productLists.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    productLists = query.IsDecsending ? productLists.OrderByDescending(
                        s => s.Name) : productLists.OrderBy(
                            s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await productLists.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public Task<ProductList?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductList?> UpdateAsync(int id, ProductListDTO protoRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}