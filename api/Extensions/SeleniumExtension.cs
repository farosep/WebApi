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


            List<Task> tasks = new List<Task>();

            foreach (int i in Enumerable.Range(2, pagesInCategory - 1))
            {
                bool isStarted = false;
                for (int count = 0; count < 100; count++)
                {
                    if (tasks.Count < 3)
                    {
                        var t = new Task(() =>
                        {
                            var strs = ParsePage($"https://magnit.ru/catalog/?pageNumber={i}&categoryId=4834").ToList();
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
            // тут могут прийти не все данные ( одна страница пустая )  - хз почему
            return answer;
        }

        private static List<string> ParsePage(string url)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                var webdriver = new ChromeDriver();
                var wait = new WebDriverWait(webdriver, new TimeSpan(0, 0, 30));
                try
                {
                    webdriver.Url = url;
                    list = wait.Until<List<string>>((d) =>
                    {
                        return d.FindElements(By.XPath(".//*[@class='new-card-product']")).Select(
                                    e => e.FindElement(By.XPath(" .//*[@class='new-card-product__title']")).Text + " " +
                                    e.FindElement(By.XPath(" .//*[@class='new-card-product__price ']/div[1]")).Text).ToList();
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