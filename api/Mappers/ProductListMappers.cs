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
                Name = PLModel.Name
            };
        }

        public static ProductList ToProductListFromDTO(this ProductListDTO PLDto,
            ApplicationDBContext _context)
        {
            return new ProductList
            {
                Name = PLDto.Name
            };
        }
    }
}