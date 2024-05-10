using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Extensions.Magnit
{
    public static class MagnitMapExtension
    {
        public static Dictionary<string, string> MagnitDict = new Dictionary<string, string>
        {
            {"CategoryPageWithPageNumber","https://magnit.ru/catalog/?pageNumber={i}&categoryId={categoryId}"},



            {"PagesInCategoryXpath",".//*[@class='paginate__container']/li[@class='num'][last()]"},
            {"ProductCardXpath",".//*[@class='new-card-product']"},
            {"ProductCardTitleXpath",".//*[@class='new-card-product__title']"},
            {"ProductCardPriceMain",".//*[@class='new-card-product__price ']/div[1]"},



            {"MilkCategoryId","4834"},
            {"BreadCategoryId","5269"},

        };

        public static string MilkUrlId = "4834";

        public static string BreadUrlId = "5269";
    }
}