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
            "Творог",
            "Масло сливочное",
            "Сгущенка",
            "Сливки",
            "Пудинг",
            "Масло сладкосливочное",
            "Масло топленое",
            "Напиток растительный",
            "Крем",
            "Желе",
            "Масло сладко-сливочное"
        };
    }
}