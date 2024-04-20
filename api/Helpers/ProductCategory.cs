using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public static class ProductCategory
    {
        // тут нужно перейти на регекс в названиях потому что некоторые имеют формат 
        //||Часть категории|| ||Название|| ||часть категории||
        public static List<string> Categories = new List<string>
        {
            @"Творог",
            @"Масло.{1,30}сливочное",
            @"Сгущенка",
            @"Сливки",
            @"Пудинг",
            @"Масло.{1,30}сладко.?сливочное",
            @"Масло.{1,30}топленое",
            @"Напиток.{1,30}растительный",
            @"Крем.{1,30}взбитый",
            @"Желе",
            @"Яйцо"
        };
    }
}