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
        public static (int?, string) GetLiquid(this string str)
        {
            var preVolume = Regex.Match(str, @"\d{1,6}мл").Value;
            if (preVolume != "")
            {
                var volume = preVolume.Remove(preVolume.Length - 2, 2);
                return (
                    int.Parse(volume),
                    str.Replace(preVolume, "")
                    );
            }


            preVolume = Regex.Match(str, @"\d{1,6}л").Value;
            if (preVolume != "")
            {
                var volume = preVolume.Remove(preVolume.Length - 1, 1);
                return (
                    int.Parse(volume) * 1000,
                    str.Replace(preVolume, "")
                    );
            }

            return (null, str);
        }

        public static (int?, string) GetWeight(this string str)
        {
            string preweight = Regex.Match(str, @"\\d{1,6}кг").Value;
            if (preweight != "")
            {
                var weight = preweight.Remove(preweight.Length - 2, 2);
                return (
                    int.Parse(weight) * 1000,
                    str.Replace(preweight, "")
                    );
            }


            preweight = Regex.Match(str, @"\s\d{1,6}г").Value;
            if (preweight != "")
            {
                var weight = preweight.Remove(preweight.Length - 1, 1);
                return (
                    int.Parse(weight),
                    str.Replace(preweight, "")
                    );
            }
            return (null, str);
        }

        public static (int?, string) GetAmount(this string str)
        {
            string preAmount = Regex.Match(str, @"\d{1,6}шт").Value;
            if (preAmount != "")
            {
                string? amount = preAmount.Remove(preAmount.Length - 2, 2);
                return (
                    int.Parse(amount),
                    str.Replace(preAmount, "")
                    );
            }
            return (null, str);
        }

        public static (float?, string) GetPercent(this string str)
        {
            string prePercent = Regex.Match(str, @"\d{1,2}.?\d?\%").Value;
            if (prePercent != "")
            {
                string? percent = prePercent.Remove(prePercent.Length - 1, 1);
                return (
                    float.Parse(percent.Replace('.', ',')),
                    str.Replace(prePercent, "")
                );
            }
            return (null, str);
        }

        public static (float?, string) GetPrice(this string str)
        {
            var prePrice = Regex.Match(str, @"\d{1,10},\d{2}\s?₽").Value;
            if (prePrice != "")
            {
                return (
                    float.Parse(prePrice.Remove(prePrice.Length - 1, 1)),
                    str.Replace(prePrice, "")
                );
            }

            return (null, str);
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

        public static bool IsPercent(this string str)
        {
            var v = Regex.Match(str, @"\d{1,2}.?\d?\%").Value;
            if (v != "") return true;
            return false;
        }
    }
}