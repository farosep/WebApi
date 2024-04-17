using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IProtoRepository<T, G>
    {
        Task<List<T>> GetAllAsync(AppUser appUser, QueryObject query);

        Task<T?> GetByIdAsync(AppUser appUser, int id);

        Task<T> CreateAsync(T protoModel);

        Task<T?> UpdateAsync(AppUser appUser, int id, G protoRequestDTO);

        Task<T?> DeleteAsync(int id, AppUser appUser);

        Task<bool> IsExist(int id);
    }
}