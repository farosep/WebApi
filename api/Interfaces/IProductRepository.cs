using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.DTO.ProductDTOs;
using api.Models;

namespace api.Interfaces
{
    public interface IProductRepository : IProtoRepository<Product, ProductRequestDTO>
    {
        public Task<Product?> CreateAndUpdateAsync(
            string name,
            int? volume,
            string? percent,
            string? category,
            string? subCategory,
            string? brand,
            int? amount,
            int? weight,
            float? price
            );

        public Task<(string, int?, string?, string?, string?, string?, int?, int?, float?)> GetInfoFromTextAsync(
            string str, string category, List<string> subCategories, List<string> brands
            );

    }


}