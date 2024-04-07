using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public int? StockId { get; set; }
        // navigation 
        public Stock? Stock { get; set; }

    }
}