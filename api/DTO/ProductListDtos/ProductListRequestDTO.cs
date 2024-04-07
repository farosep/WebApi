using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;

namespace api.DTO.ProductListDTOs
{
    public class ProductListRequestDTO : ProtoRequestDTO
    {
        public Guid Guid { get; set; }

        public int UserId { get; set; }
    }
}