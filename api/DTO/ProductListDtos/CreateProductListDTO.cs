using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;

namespace api.DTO.ProductListDtos
{
    public class CreateProductListDTO : ProtoDTO
    {
        public int UserId { get; set; }
    }
}