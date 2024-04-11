using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProductDTOs;
using api.DTO.ProtoDTOS;
using api.Models;

namespace api.DTO.ProductListDTOs
{
    public class ProductListDTO : ProtoDTO
    {
        public Guid Guid { get; set; }

        public int UserId { get; set; }

        public List<ProductDTO> Products { get; set; } = [];
    }
}