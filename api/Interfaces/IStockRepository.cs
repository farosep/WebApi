using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.StockDTOs;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository : IProtoRepository<Stock, StockRequestDTO>
    {
    }
}