using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.ProductList
{
    // Это набор данных которые мы отправляем от пользователя
    public class CreateProductListRequestDTO
    {
        public int UserId { get; set; }
    }
}