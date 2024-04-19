using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Product : BaseEntity
    {
        public float? MagnitPrice { get; set; }

        public int? Weight { get; set; }

        public int? Volume { get; set; }

        public float? PiaterochkaPrice { get; set; }

        public int? Amount { get; set; }

        public List<PLPModel> ProductLists { get; set; } = new List<PLPModel>();
    }
}