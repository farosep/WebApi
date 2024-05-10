using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;

namespace api.Models
{
    public class Product : BaseEntity
    {
        public float? MagnitPrice { get; set; }
        public float? LentaPrice { get; set; }

        public int? Weight { get; set; }

        public int? Volume { get; set; }

        public string? Brand { get; set; }

        public string? Percent { get; set; }

        public string? Category { get; set; }

        public string? SubCategory { get; set; }

        public int? Amount { get; set; }

        public List<PLPModel> ProductLists { get; set; } = new List<PLPModel>();
    }
}