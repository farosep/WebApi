using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace api.Extensions
{
    public static class SeleniumExtension
    {
        public static async Task<List<string>> GetInfoFromCategory(string Url)
        {
            List<string> answer = new List<string>();

            var webdriver = new ChromeDriver();

            webdriver.Url = Url;

            var pagesInCategory = int.Parse(webdriver.FindElement(By.XPath(
                ".//*[@class='paginate__container']/li[@class='num'][last()]")).Text);

            var list = webdriver.FindElements(By.XPath(".//*[@class='new-card-product']")).Select(
                e => e.FindElement(By.XPath(" .//*[@class='new-card-product__title']")).Text + " " +
                e.FindElement(By.XPath(" .//*[@class='new-card-product__price ']/div[1]")).Text).ToList();
            webdriver.Quit();

            foreach (int i in Enumerable.Range(2, pagesInCategory - 1))
            {
                answer = answer.Concat(await ParsePage(
                        $"https://magnit.ru/catalog/?pageNumber={i}&categoryId=4834"))
                .ToList();
            }

            return answer;
        }

        private async static Task<List<string>> ParsePage(string url)
        {
            var webdriver = new ChromeDriver();
            webdriver.Url = url;
            var list = webdriver.FindElements(By.XPath(".//*[@class='new-card-product']")).Select(
                e => e.FindElement(By.XPath(" .//*[@class='new-card-product__title']")).Text + " " +
                e.FindElement(By.XPath(" .//*[@class='new-card-product__price ']/div[1]")).Text).ToList();
            webdriver.Quit();
            return list;
        }
    }
}