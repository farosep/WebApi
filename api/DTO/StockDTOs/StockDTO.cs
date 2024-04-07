using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.CommentDTOs;
using api.DTO.ProtoDTOS;

namespace api.DTO.StockDTOs
{
    public class StockDTO : ProtoDTO
    {

        public string Symbol { get; set; } = string.Empty;

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<CommentDTO> Comments { get; set; }
    }
}