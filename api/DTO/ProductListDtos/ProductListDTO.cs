using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;
using api.Models;

namespace api.DTO.ProductListDTOs
{
    public class ProductListDTO : ProtoRequestDTO
    {
        public string UserId { get; set; }

        public List<int> ProductsIds { get; set; } = [];
    }
}