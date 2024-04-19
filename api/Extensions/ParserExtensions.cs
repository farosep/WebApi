using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ParserExtensions
    {

        // -2 И -3 ГОВНО, НАДО ЧЁТО ПРИДУМАТЬ ПОТОМ ИБО ХЕР РАЗБЕРЕТ ЗАЧЕМ ОНО НАДО ЧЕРЕЗ ПОЛ ГОДА
        public static (int?, string?) ConvertToLiquid(this string str)
        {
            var preVolume = Regex.Match(str, @"\s\d{1,6}мл\s").Value;
            if (preVolume != "")
            {
                var sep = preVolume.Remove(preVolume.Length - 3, 3);
                return (int.Parse(sep), sep);
            }


            preVolume = Regex.Match(str, @"\s\d{1,6}л\s").Value;
            if (preVolume != "")
            {
                var sep = preVolume.Remove(preVolume.Length - 2, 2);
                return (int.Parse(sep) * 1000, sep);
            }

            return (null, null);
        }

        public static (int?, string?) ConvertToWeight(this string str)
        {
            string preweight = Regex.Match(str, @"\s\d{1,6}кг\s").Value;
            if (preweight != "")
            {
                var sep = preweight.Remove(preweight.Length - 3, 3);
                return (int.Parse(sep) * 1000, sep);
            }


            preweight = Regex.Match(str, @"\s\d{1,6}г\s").Value;
            if (preweight != "")
            {
                var sep = preweight.Remove(preweight.Length - 2, 2);
                return (int.Parse(sep), sep);
            }
            return (null, null);
        }

        public static (int?, string?) ConvertToAmount(this string str)
        {
            string preAmount = Regex.Match(str, @"\s\d{1,6}шт\s").Value;
            if (preAmount != "")
            {
                var sep = preAmount.Remove(preAmount.Length - 3, 3);
                return (int.Parse(sep), sep);
            }
            return (null, null);
        }

        public static string ConvertToName(this string str, string separator)
        {
            return str.Split(separator)[0];
        }

        public static float ConvertToPrice(this string str)
        {
            return float.Parse(Regex.Match(str, @"\d{1,10},\d{2}").Value);
        }

        public static bool IsLiquid(this string str)
        {
            var v1 = Regex.Match(str, @"\s\d{1,6}мл\s").Value;
            var v2 = Regex.Match(str, @"\s\d{1,6}л\s").Value;
            if (v1 != "" || v2 != "") return true;
            else return false;
        }

        public static bool IsSolid(this string str)
        {
            var v1 = Regex.Match(str, @"\s\d{1,6}кг\s").Value;
            var v2 = Regex.Match(str, @"\s\d{1,6}г\s").Value;
            if (v1 != "" || v2 != "") return true;
            else return false;
        }

        public static bool IsAmount(this string str)
        {
            var v = Regex.Match(str, @"\s\d{1,6}шт\s").Value;
            if (v != "") return true;
            return false;
        }
    }
}