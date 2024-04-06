using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.ProductListDTOs
{
    public class ProductListRequestDTO
    {
        public Guid Guid { get; set; }

        public int UserId { get; set; }
    }
}