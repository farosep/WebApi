using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProductList;
using api.Models;

namespace api.Mappers
{
    public static class ProductListMappers
    {
        public static ProductListDTO ToProductListDto(this ProductList PLModel)
        {
            return new ProductListDTO
            {
                Id = PLModel.Id,
                UserId = PLModel.UserId,
                Guid = PLModel.Guid
            };
        }

        public static ProductList ToProductListFromCreateDTO(this ProductListRequestDTO PLDto)
        {
            return new ProductList
            {
                UserId = PLDto.UserId,
                Guid = Guid.NewGuid()
            };
        }
    }
}