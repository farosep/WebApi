using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;
using api.DTO.StockDTOs;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IProtoRepository<T, G>
    {
        Task<List<T>> GetAllAsync(QueryObject query);

        Task<T?> GetByIdAsync(int id);

        Task<T> CreateAsync(T protoModel);

        Task<T?> UpdateAsync(int id, G protoRequestDTO);

        Task<T?> DeleteAsync(int id);

        Task<bool> IsExist(int id);
    }
}