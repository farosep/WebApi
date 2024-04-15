using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio // модель для связи many-to-many юзеров и стоков
    {

        public string AppUserId { get; set; }

        public int StockId { get; set; }

        public AppUser appUser { get; set; }

        public Stock stock { get; set; }
    }
}