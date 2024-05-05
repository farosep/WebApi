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
        public float? MagnitPrice { get; set; }

        public int? Weight { get; set; }

        public int? Volume { get; set; }
        public string? Percent { get; set; }
        public string? SubCategory { get; set; }

        public string? Brand { get; set; }

        public string? Category { get; set; }
        public int? Amount { get; set; }

        public float? PiaterochkaPrice { get; set; }
    }
}