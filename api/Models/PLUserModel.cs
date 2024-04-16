using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{// модель таблицы с ключами для связывания товаров и списков товаров
    [Table("PLUserModel")]
    public class PLUserModel
    {
        public string UserId { get; set; }

        public int ProductListId { get; set; }

        public ProductList productList { get; set; }

        public AppUser user { get; set; }
    }
}