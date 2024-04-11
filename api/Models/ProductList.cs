using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class ProductList : BaseEntity
    {
        public int UserId { get; set; }

        public List<Product> Products { get; set; }


    }
}