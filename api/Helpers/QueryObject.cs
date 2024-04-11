using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    // добавляется для фильтрации, в хедеры выносятся переменные из бади и 
    //если заполнено гет вернет только то что подходит 
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;

        public string? Name { get; set; } = null;
    }
}