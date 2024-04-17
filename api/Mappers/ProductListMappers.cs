using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductListDtos;
using api.DTO.ProductListDTOs;
using api.Models;

namespace api.Mappers
{
    public static class ProductListMappers
    {
        public static ProductListDTO FromProductListToDTO(this ProductList PLModel, AppUser appUser)
        {
            return new ProductListDTO
            {
                Id = PLModel.Id,
                Name = PLModel.Name,
                UserId = appUser.Id,
                ProductsIds = PLModel.Products.Select(
                    p => p.ProductId)
                        .ToList()
            };
        }

        public static ProductList ToProductListFromDTO(this UpdateProductListDTO PLDto, AppUser appUser)
        {
            var pl = new ProductList
            {
                Name = PLDto.Name
            };

            pl.User = new PLUserModel
            {
                UserId = appUser.Id,
                ProductListId = pl.Id
            };

            foreach (int i in PLDto.ProductsIds.Distinct())
            {
                pl.Products.Add(new PLPModel
                {
                    ProductId = i,
                    ProductListId = pl.Id
                });
            }

            return pl;
        }
    }
}