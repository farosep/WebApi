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
            var preVolume = Regex.Match(str, @"\d{1,2}.(\d{1,3})мл").Value;
            if (preVolume != "")
            {
                var volume = preVolume.Remove(preVolume.Length - 2, 2);
                return (
                    int.Parse(volume.Replace(".", "")),
                    str.Replace(preVolume, "")
                    );
            }


            preVolume = Regex.Match(str, @"\d{1,6}мл").Value;
            if (preVolume != "")
            {
                var volume = preVolume.Remove(preVolume.Length - 2, 2);
                return (
                    int.Parse(volume),
                    str.Replace(preVolume, "")
                    );
            }


            preVolume = Regex.Match(str, @"\d{1,2}.(\d{1,3})л").Value;
            if (preVolume != "")
            {
                var volume = preVolume.Remove(preVolume.Length - 1, 1);
                return (
                    int.Parse(volume.Replace(".", "")) * 100,
                    str.Replace(preVolume, "")
                    );
            }

            preVolume = Regex.Match(str, @"\d{1,3}л").Value;
            if (preVolume != "")
            {
                var volume = preVolume.Remove(preVolume.Length - 1, 1);
                return (
                    int.Parse(volume.Replace(".", "")) * 1000,
                    str.Replace(preVolume, "")
                    );
            }

            return (null, str);
        }

        public static (int?, string) GetWeight(this string str)
        {
            string preweight = Regex.Match(str, @"\d{0,2}?\.\d{1,3}кг").Value;
            if (preweight != "")
            {
                var weight = preweight.Remove(preweight.Length - 2, 2);
                return (
                    int.Parse(weight.Replace(".", "")) * 100,
                    str.Replace(preweight, "")
                    );
            }

            preweight = Regex.Match(str, @"\d{1,3}кг").Value;
            if (preweight != "")
            {
                var weight = preweight.Remove(preweight.Length - 2, 2);
                return (
                    int.Parse(weight.Replace(".", "")) * 1000,
                    str.Replace(preweight, "")
                    );
            }


            preweight = Regex.Match(str, @"\d{0,1}?\.?\d{1,6}г").Value;
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

        public static (string?, string) GetPercent(this string str)
        {
            string prePercent = Regex.Match(str, @"\d{1,2}\.?\d?\%?-\d{1,2}\.?\d?\%").Value;
            if (prePercent != "")
            {
                return (
                    prePercent,
                    str.Replace(prePercent, "")
                );
            }


            prePercent = Regex.Match(str, @"\d{0,2}\.?\d{1,2}?\%").Value;
            if (prePercent != "")
            {
                return (
                    prePercent,
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
                    str.Replace(prePrice, "").ToLower()
                );
            }

            return (null, str);
        }

        public static (string?, string?, string) GetCategoryAndSubCategory(this string str, List<string> categories, List<string> subCtegories)
        {
            foreach (string category in categories)
            {
                if (Regex.IsMatch(str, category))
                {
                    var catString = Regex.Match(str, category).Value.TrimEnd('-');
                    foreach (string subCat in subCtegories)
                    {
                        if (Regex.IsMatch(str, subCat))
                        {
                            var subCatStr = Regex.Match(str, subCat).Value.TrimEnd('-');
                            return (
                                catString,
                                subCatStr,
                                str.Replace(catString, ""));
                        }
                    }
                    return (
                        catString,
                        null,
                        str.Replace(catString, ""));
                }
            }
            return (null, null, str);
        }

        public static bool IsLiquid(this string str)
        {
            var v1 = Regex.Match(str, @"\d{1,6}мл\s").Value;
            var v2 = Regex.Match(str, @"\d{1,6}л\s").Value;
            if (v1 != "" || v2 != "") return true;
            else return false;
        }

        public static bool IsSolid(this string str)
        {
            var v1 = Regex.Match(str, @"\d{1,6}кг\s").Value;
            var v2 = Regex.Match(str, @"\d{1,6}г\s").Value;
            if (v1 != "" || v2 != "") return true;
            else return false;
        }

        public static bool IsAmount(this string str)
        {
            var v = Regex.Match(str, @"\d{1,6}шт").Value;
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