using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Product : BaseEntity
    {
        public float MagnitPrice { get; set; }

        public float Weight { get; set; }

        public float PiaterochkaPrice { get; set; }

        public List<PLPModel> PLPModel { get; set; } = new List<PLPModel>();
    }
}