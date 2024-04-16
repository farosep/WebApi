using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PLUserRepository : IPLUserRepository
    {
        private readonly ApplicationDBContext _context;

        public PLUserRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool> IsOwnerOfPL(int plId, string userId)
        {
            return await _context.PLUserModels.AnyAsync(i => i.ProductListId == plId && i.UserId == userId);
        }
    }
}