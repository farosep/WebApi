using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;
using api.Models;

namespace api.DTO.ProductListDTOs
{
    public class ProductListRequestDTO : ProtoRequestDTO
    {
        public int UserId { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}