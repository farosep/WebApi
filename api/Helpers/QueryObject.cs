using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    // добавлен для фильтрации, сортировки и пагинации
    public class QueryObject
    {
        // это поля для фильтрациив хедеры выносятся переменные из бади и 
        //если заполнено гет вернет только то что подходит 
        public string? Name { get; set; } = null;


        // эти 2 поля для сортировки, в строке используется для опрееделения по какому принципу сортировать
        // булка определяет по убыванию или по возрастанию выдавать результат
        public string? SortBy { get; set; } = null;

        public bool IsDecsending { get; set; } = false;

        // для пагинации ( разбития на страницы)
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}