using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProductDTOs;
using api.Models;

namespace api.Interfaces
{
    public interface IProductRepository : IProtoRepository<Product, ProductRequestDTO>
    {

    }
}