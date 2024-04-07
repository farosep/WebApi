using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProductListDTOs;
using api.Models;

namespace api.Interfaces
{
    public interface IProductListRepository : IProtoRepository<ProductList, ProductListRequestDTO>
    {
    }
}