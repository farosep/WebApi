using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace api.Extensions
{
    public static class SeleniumExtension
    {
        public static async Task<List<string>> GetInfoFromCategory(string categoryId)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            var pagesInCategory = FingCountOfPagesInCategory(chromeOptions, categoryId);

            List<string> answer = new List<string>();
            List<Task> tasks = new List<Task>();

            foreach (int i in pagesInCategory)
            {
                bool isStarted = false;
                for (int count = 0; count < 100; count++)
                {
                    if (tasks.Count < 12)
                    {
                        var t = new Task(() =>
                        {
                            var strs = ParsePage($"https://magnit.ru/catalog/?pageNumber={i}&categoryId={categoryId}", chromeOptions).ToList();
                            foreach (string str in strs)
                            {
                                answer.Add(str);
                            }
                        });
                        t.Start();
                        tasks.Add(t);
                        isStarted = true;
                        break;
                    }
                    foreach (Task t in tasks)
                    {
                        await t;
                        tasks.Remove(t);
                        break;
                    }
                }
                if (!isStarted)
                {
                    Console.WriteLine($"не стартанула {i} страница");
                }
            }
            foreach (Task t in tasks)
            {
                await t;
            }
            return answer;
        }

        private static List<string> ParsePage(string url, ChromeOptions chromeOptions)
        {
            var webdriver = new ChromeDriver(chromeOptions);
            var wait = new WebDriverWait(webdriver, new TimeSpan(0, 0, 30));

            var answer = FindInfoAboutProducts(webdriver, wait, url);


            return answer;
        }


        private static List<string> FindInfoAboutProducts(IWebDriver webdriver, WebDriverWait wait, string url, int repeat = 5)
        {
            for (int i = 0; i < repeat; i++)
            {
                try
                {
                    webdriver.Url = url;
                    var answer = wait.Until<List<string>>((d) =>
                    {
                        var a = webdriver.FindElements(By.XPath(".//*[@class='new-card-product']")).Select(
                                        e => e.FindElement(By.XPath(" .//*[@class='new-card-product__title']")).Text + " " +
                                        e.FindElement(By.XPath(" .//*[@class='new-card-product__price ']/div[1]")).Text).ToList();
                        if (a.Count == 0)
                        {
                            throw new Exception("Нашли 0 строк");
                        }
                        return a;
                    });
                    webdriver.Quit();
                    return answer;
                }
                catch
                {

                }
            }
            return new List<string>();
        }

        private static IEnumerable<int> FingCountOfPagesInCategory(ChromeOptions chromeOptions, string categoryId, int repeat = 5)
        {
            for (int i = 0; i < repeat; i++)
            {
                try
                {
                    var webdriver = new ChromeDriver(chromeOptions);

                    webdriver.Url = $"https://magnit.ru/catalog/?&categoryId={categoryId}";

                    var pagesInCategory = Enumerable.Range(1, int.Parse(webdriver.FindElement(By.XPath(
                        ".//*[@class='paginate__container']/li[@class='num'][last()]")).Text));
                    webdriver.Quit();
                    return pagesInCategory;
                }
                catch
                {

                }
            }
            return new List<int>();
        }
    }
}