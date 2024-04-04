using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ProductList
    {
        public int Id { get; set; }
        //public Guid Guid { get; set; }

        public int UserId { get; set; }

        public List<Product> products { get; set; } = new List<Product>();
    }
}