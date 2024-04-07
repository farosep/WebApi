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
                PiaterochkaPrice = model.PiaterochkaPrice,
                CreatedOn = model.CreatedOn
            };
        }

        public static Product ToProductFromCreateDTO(this ProductRequestDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                MagnitPrice = productDTO.MagnitPrice,
                Weight = productDTO.Weight,
                PiaterochkaPrice = productDTO.PiaterochkaPrice,
                CreatedOn = DateTime.Now

            };
        }
    }
}