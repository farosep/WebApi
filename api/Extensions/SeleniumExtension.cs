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
        public static async Task<List<string>> GetInfoFromCategory(int categoryId)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var webdriver = new ChromeDriver(chromeOptions);

            webdriver.Url = $"https://magnit.ru/catalog/?&categoryId={categoryId}";

            var pagesInCategory = Enumerable.Range(1, int.Parse(webdriver.FindElement(By.XPath(
                ".//*[@class='paginate__container']/li[@class='num'][last()]")).Text));

            List<string> answer = new List<string>();
            webdriver.Quit();


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
            List<string> list = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                var webdriver = new ChromeDriver(chromeOptions);
                var wait = new WebDriverWait(webdriver, new TimeSpan(0, 0, 30));
                try
                {
                    webdriver.Url = url;
                    // тут можем получить 0 строк!!!
                    list = wait.Until<List<string>>((d) =>
                    {
                        var answer = d.FindElements(By.XPath(".//*[@class='new-card-product']")).Select(
                                    e => e.FindElement(By.XPath(" .//*[@class='new-card-product__title']")).Text + " " +
                                    e.FindElement(By.XPath(" .//*[@class='new-card-product__price ']/div[1]")).Text).ToList();
                        if (answer.Count == 0)
                        {
                            throw new Exception("Нашли 0 строк");
                        }
                        return answer;
                    });
                    webdriver.Quit();
                    break;
                }
                catch
                {
                    webdriver.Quit();
                }
            }

            return list;
        }
    }
}