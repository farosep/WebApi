using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    // модель таблицы с ключами для связывания товаров и списков товаров
    [Table("PLPModel")]
    public class PLPModel
    {
        public int ProductListId { get; set; }

        public int ProductId { get; set; }

        public ProductList ProductList { get; set; }

        public Product Product { get; set; }
    }
}