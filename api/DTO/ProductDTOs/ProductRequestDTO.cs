using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;
using api.Models;

namespace api.DTO.ProductDTOs
{
    public class ProductRequestDTO : ProtoRequestDTO
    {
        public float? MagnitPrice { get; set; }

        public int? Volume { get; set; }

        public int? Amount { get; set; }

        public int? Weight { get; set; }

        public string? Category { get; set; }

        public float? Percent { get; set; }

        public float? PiaterochkaPrice { get; set; }
    }
}