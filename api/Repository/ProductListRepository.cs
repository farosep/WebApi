using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductListDtos;
using api.DTO.ProductListDTOs;
using api.Helpers;
using api.Interfaces;
using api.Migrations;
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
            var productLists = _context.PLUserModels
                .Where(u => u.UserId == appUser.Id)
                .Select(pl => new ProductList
                {
                    Id = pl.productList.Id,
                    Name = pl.productList.Name,
                    Products = _context.PLPproductsTable
                        .Where(p => p.ProductListId == pl.ProductListId)
                        .ToList()
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

        public async Task<ProductList?> GetByIdAsync(AppUser appUser, int id)
        {
            var productList = await _context.PLUserModels
            .Where(u => u.UserId == appUser.Id && u.productList.Id == id)
            .Select(pl => new ProductList
            {
                Id = pl.productList.Id,
                Name = pl.productList.Name,
                Products = _context.PLPproductsTable
                        .Where(p => p.ProductListId == pl.ProductListId)
                        .ToList()
            }).ToListAsync();
            if (productList.Count != 0) return productList[0];
            return null;
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductList?> UpdateAsync(AppUser appUser, int id, UpdateProductListDTO RequestDTO)
        {
            var productList = _context.ProductLists
                .Where(x => x.Id == id)
                .Include(p => p.Products)
                .Include(u => u.User).ToList()[0];

            if (productList == null || productList.User.UserId != appUser.Id) return null;

            if (RequestDTO.Name != "") productList.Name = RequestDTO.Name;
            if (RequestDTO.ProductsIds != new List<int>())
            {
                var tableOld = _context.PLPproductsTable
                    .Where(x => x.ProductListId == id)
                    .Select(x => x.ProductId)
                    .ToList();

                // найти отличия от имеющегося
                var deletePart = tableOld.Except(RequestDTO.ProductsIds).ToList();
                var addPart = RequestDTO.ProductsIds.Except(tableOld).ToList();
                // новое добавить
                foreach (int number in addPart)
                {
                    productList.Products.Add(new PLPModel
                    {
                        ProductListId = productList.Id,
                        ProductId = number
                    });
                }

                // старое удалить
                foreach (int number in deletePart)
                {
                    _context.PLPproductsTable.Remove(
                        productList.Products.FirstOrDefault(
                            p => p.ProductId == number));
                }
            }
            await _context.SaveChangesAsync();

            return productList;
        }
    }
}