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
        public Task<Product?> CreateAndUpdateAsync(ProductDTO productDTO);

        public Task<ProductDTO> GetInfoFromTextAsync(
            string str, string category, List<string> subCategories, List<string> brands, string shop
            );

    }


}