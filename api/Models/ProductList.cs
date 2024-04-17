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
        public List<PLPModel> Products { get; set; } = new List<PLPModel>();

        public PLUserModel User { get; set; }
    }
}