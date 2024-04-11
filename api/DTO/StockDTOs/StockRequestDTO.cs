using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;

namespace api.DTO.StockDTOs
{
    public class StockRequestDTO : ProtoRequestDTO
    {
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [Range(1, 100)]
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
    }
}