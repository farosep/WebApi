using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;

namespace api.DTO.ProductListDtos
{
    public class UpdateProductListDTO : ProtoDTO
    {
        public int UserId { get; set; }

        public List<int> ProductsIds { get; set; } = [];
    }
}