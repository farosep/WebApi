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
            float? percent,
            string? category,
            int? amount,
            int? weight,
            float? price
            );

        public Task<(string, int?, float?, string?, int?, int?, float?)> GetInfoFromTextAsync(string str);

    }


}