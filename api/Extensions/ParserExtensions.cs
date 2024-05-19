using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api.Helpers;
using api.Migrations;

namespace api.Extensions
{
    public static class ParserExtensions
    {
        // -2 И -3 ГОВНО, НАДО ЧЁТО ПРИДУМАТЬ ПОТОМ ИБО ХЕР РАЗБЕРЕТ ЗАЧЕМ ОНО НАДО ЧЕРЕЗ ПОЛ ГОДА
        public static (int?, string) GetLiquid(this string str)
        {
            var preVolume = Regex.Match(str, @"\d{0,3}.?(\d{1,3})м?л").Value;
            if (preVolume != "")
            {
                if (preVolume.Contains('м'))
                {
                    var volume = preVolume.Remove(preVolume.Length - 2, 2);
                    return (
                        int.Parse(volume.Replace(".", "")),
                        str.Replace(preVolume, "")
                        );
                }
                var volumeStr = preVolume.Remove(preVolume.Length - 1, 1);
                var volumeFloat = float.Parse(volumeStr.Replace(".", ",")) * 1000;
                return (
                    (int)volumeFloat,
                    str.Replace(preVolume, "")
                    );

            }
            return (0, str);
        }

        public static (int?, string) GetWeight(this string str)
        {
            string preweight = Regex.Match(str, @"\d{0,3}\.?(\d{1,3})/?к?г").Value;
            if (preweight != "")
            {
                if (preweight.Contains("/кг"))
                {
                    return (
                        1000,
                        str.Replace("/кг", "₽")
                    );
                }
                if (preweight.Contains('к'))
                {
                    var weightStr = preweight.Remove(preweight.Length - 2, 2);
                    var weightFloat = float.Parse(weightStr.Replace(".", ",")) * 1000;
                    return (
                        (int)weightFloat,
                        str.Replace(preweight, "")
                        );
                }
                var weight = preweight.Remove(preweight.Length - 1, 1);
                return (
                    int.Parse(weight.Replace(".", "")),
                    str.Replace(preweight, "")
                    );

            }

            return (0, str);
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
            return (0, str);
        }

        public static (string?, string) GetPercent(this string str)
        {
            string prePercent = Regex.Match(str, @"\d{0,2}\.?\d?\%?-?\d{0,2}\.?\d\%").Value;
            if (prePercent != "")
            {
                return (
                    prePercent,
                    str.Replace(prePercent, "")
                );
            }
            return ("", str);
        }

        public static (float?, string) GetPrice(this string str)
        {
            var prePrice = Regex.Match(str, @"\d{1,10},\d{2}\s?₽").Value;
            if (prePrice != "")
            {
                return (
                    float.Parse(prePrice.Remove(prePrice.Length - 1, 1)),
                    str.Replace(prePrice, "").ToLower()
                );
            }

            return (null, str);
        }

        public static (string?, string) GetBrand(this string str, List<string> brands)
        {
            foreach (string brand in brands)
            {
                var v = Regex.Match(str, brand).Value;
                if (v != "")
                {
                    return (brand, str.Replace(brand, ""));
                }
            }
            return ("", str);
        }
        public static string? GetSubCategory(this string str, List<string> subCtegories)
        {
            foreach (string subCat in subCtegories)
            {
                if (Regex.IsMatch(str, subCat))
                {
                    var subCatStr = Regex.Match(str, subCat).Value.TrimEnd('-');
                    return (subCatStr);
                }
            }
            return ("");
        }


        public static bool IsLiquid(this string str)
        {
            var p = Regex.Match(str, @"\d{1,6}м?л").Value;
            if (p != "") return true;
            else return false;
        }

        public static bool IsSolid(this string str)
        {
            var p = Regex.Match(str, @"\d{1,6}к?г").Value;
            if (p != "") return true;
            else return false;
        }

        public static bool IsAmount(this string str)
        {
            var p = Regex.Match(str, @"\d{1,6}шт").Value;
            if (p != "") return true;
            return false;
        }

        public static bool IsPercent(this string str)
        {
            var p = Regex.Match(str, @"\d{1,2}.?\d\%").Value;
            if (p != "") return true;
            return false;
        }


    }
}