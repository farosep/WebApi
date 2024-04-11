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
        public static ProductListDTO ToProductListDto(this ProductList PLModel)
        {
            return new ProductListDTO
            {
                Id = PLModel.Id,
                UserId = PLModel.UserId,
                Products = PLModel.Products.Select(p => p.ToProductDTOWithNoProductLists()).ToList()
            };
        }

        public static ProductListDTO ToProductListDtoWithNoProduct(this ProductList PLModel)
        {
            return new ProductListDTO
            {
                Id = PLModel.Id,
                UserId = PLModel.UserId
            };
        }

        public static ProductList ToProductListFromCreateDTO(this CreateProductListRequestDTO PLDto,
            ApplicationDBContext _context)
        {
            return new ProductList
            {
                UserId = PLDto.UserId,
                Products = PLDto.ProductIds.Select(
                    id => _context.Products.FirstOrDefault(
                        p => p.Id == id)).ToList()
            };
        }
    }
}