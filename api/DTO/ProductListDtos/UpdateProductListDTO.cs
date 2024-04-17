using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.ProductListDtos
{
    public class UpdateProductListDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<int> ProductsIds { get; set; } = [];
    }
}