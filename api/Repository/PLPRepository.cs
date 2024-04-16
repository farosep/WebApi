using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class PLPRepository : IPLPRepository
    {
        public Task<List<Product>> GetProductList()
        {
            throw new NotImplementedException();
        }
    }
}