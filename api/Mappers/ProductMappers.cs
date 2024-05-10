using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProductDTOs;
using api.Models;

namespace api.Mappers
{
    public static class ProductMappers
    {
        public static ProductDTO ToProductDTO(this Product model)
        {
            return new ProductDTO
            {
                Id = model.Id,
                Name = model.Name,
                MagnitPrice = model.MagnitPrice,
                Weight = model.Weight,
                Brand = model.Brand,
                SubCategory = model.SubCategory,
                Percent = model.Percent,
                Category = model.Category,
                Amount = model.Amount,
                Volume = model.Volume,
                LentaPrice = model.LentaPrice
            };
        }

        public static Product ToProductFromCreateDTO(this ProductRequestDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                MagnitPrice = productDTO.MagnitPrice,
                Amount = productDTO.Amount,
                Percent = productDTO.Percent,
                Category = productDTO.Category,
                Brand = productDTO.Brand,
                SubCategory = productDTO.SubCategory,
                Volume = productDTO.Volume,
                Weight = productDTO.Weight,
                LentaPrice = productDTO.LentaPrice
            };
        }
    }
}