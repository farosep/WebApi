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
            {"ProductCardVolumeXpath",""}, // в магните всё в тайтле 
            {"ProductCardPriceMainXpath",".//*[@class='new-card-product__price ']/div[1]"},
            {"ProductCardPriceAdditionalXpath",""}, // в магните всё в тайтле 
            {"RubleSymbolXpath",""}, // в магните всё в тайтле 



            {"MilkCategoryId","4834"},
            {"BreadCategoryId","5269"},

        };

        public static string MilkUrlId = "4834";
        public static string BreadUrlId = "5269";

        public static Dictionary<string, string> LentaDict = new Dictionary<string, string>
        {
            {"CategoryPageWithPageNumber","https://lenta.com/catalog/{categoryId}/?page={i}"},

            {"PagesInCategoryXpath",".//*[@class='Pagination_pagesWrapper__f2JlY']/button[last()]"},
            {"ProductCardXpath",".//*[@class='ProductCard_productCard__mfzu7 Card_card__1Xuhi']"},
            {"ProductCardTitleXpath",".//*[@class='ProductCard_productCard__mfzu7 Card_card__1Xuhi']/span[1]"},
            {"ProductCardVolumeXpath",".//*[@class='ProductCard_productCard__mfzu7 Card_card__1Xuhi']/span[2]"},
            {"ProductCardPriceMainXpath",".//*[@class='ProductCard_productCardPrices__1_teX']/div[1]"},
            {"ProductCardPriceAdditionalXpath",".//*[@class='ProductCard_productCardPrices__1_teX']/div[1]/span[2]"},
            {"RubleSymbolXpath",".//*[@class='PriceText_priceTextCurrency__NxzIc']"},

            {"MilkCategoryId","moloko-syr-yajjco"},
            {"BreadCategoryId",""},
        };

    }
}