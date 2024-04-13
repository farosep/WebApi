using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductListDTOs;
using api.Models;

namespace api.Mappers
{
    public static class ProductListMappers
    {
        public static ProductListDTO FromProductListToDTO(this ProductList PLModel)
        {
            return new ProductListDTO
            {
                Id = PLModel.Id,
                UserId = PLModel.UserId,
                Name = PLModel.Name,
                ProductsIds = PLModel.Products.Select(p => p.Id).ToList()
            };
        }

        public static ProductList ToProductListFromDTO(this ProductListDTO PLDto,
            ApplicationDBContext _context)
        {
            return new ProductList
            {
                Name = PLDto.Name,
                UserId = PLDto.UserId,
                Products = PLDto.ProductsIds.Select(
                    id => _context.Products.FirstOrDefault(
                        p => p.Id == id)).ToList()
            };
        }
    }
}