using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")] // задаёт название таблицы, надо проделать всем моделям
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public int? StockId { get; set; }
        // navigation 
        public Stock? Stock { get; set; }

    }
}