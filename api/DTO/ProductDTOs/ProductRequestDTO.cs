using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.ProductDTOs
{
    public class ProductRequestDTO
    {
        public float MagnitPrice { get; set; }

        public float Weight { get; set; }

        public float PiaterochkaPrice { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}