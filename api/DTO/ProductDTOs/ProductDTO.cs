using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProductListDTOs;
using api.DTO.ProtoDTOS;
using api.Models;

namespace api.DTO.ProductDTOs
{
    public class ProductDTO : ProtoDTO
    {
        public float MagnitPrice { get; set; }

        public float Weight { get; set; }

        public float PiaterochkaPrice { get; set; }

        public List<ProductListDTO> ProductLists { get; set; } = [];
    }
}