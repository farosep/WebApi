using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPLUserRepository
    {
        Task<bool> IsOwnerOfPL(int plId, string userId);
    }
}