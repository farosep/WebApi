using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;

namespace api.Extensions
{
    public static class SeleniumExtension
    {
        public static async Task<List<string>> GetInfoFromCategory(string categoryId, Dictionary<string, string> shopDict)
        {
            var driverExecutablePath = $@"C:\Users\andrew\.nuget\packages\selenium.webdriver.chromedriver\124.0.6367.15500\driver\win32\chromedriver.exe";

            var pagesInCategory = FingCountOfPagesInCategory(NewChromeOptions(), categoryId, driverExecutablePath, shopDict);

            List<string> answer = new List<string>();
            List<Task> tasks = new List<Task>();

            foreach (int i in pagesInCategory)
            {
                bool isStarted = false;
                for (int count = 0; count < 100; count++)
                {
                    if (tasks.Count < 5)
                    {
                        var t = new Task(() =>
                        {
                            var strs = ParsePage(
                                shopDict["CategoryPageWithPageNumber"].Replace("{i}", $"{i}").Replace("{categoryId}", $"{categoryId}"),
                                NewChromeOptions(),
                                driverExecutablePath,
                                shopDict
                                )
                            .ToList();
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

        private static List<string> ParsePage(string url, ChromeOptions chromeOptions, string driverExecutablePath, Dictionary<string, string> shopDict)
        {
            var webdriver = UndetectedChromeDriver.Create(driverExecutablePath: driverExecutablePath, options: chromeOptions);
            var wait = new WebDriverWait(webdriver, new TimeSpan(0, 0, 30));

            var answer = FindInfoAboutProducts(webdriver, wait, url, shopDict);


            return answer;
        }


        private static List<string> FindInfoAboutProducts(UndetectedChromeDriver webdriver, WebDriverWait wait, string url, Dictionary<string, string> shopDict, int repeat = 5)
        {
            for (int i = 0; i < repeat; i++)
            {
                try
                {
                    webdriver.GoToUrl(url);
                    var answer = wait.Until<List<string>>((d) =>
                    {
                        var a = webdriver.FindElements(By.XPath(shopDict["ProductCardXpath"])).Select(
                                        e =>
                                        e.FindElementText(shopDict["ProductCardTitleXpath"]) + " " +
                                        e.FindElementText(shopDict["ProductCardPriceMain"]))
                                        .ToList();
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
            webdriver.Quit();
            return new List<string>();
        }

        private static IEnumerable<int> FingCountOfPagesInCategory(ChromeOptions chromeOptions, string categoryId, string driverExecutablePath, Dictionary<string, string> shopDict, int repeat = 5)
        {
            UndetectedChromeDriver? webdriver = UndetectedChromeDriver.Create(driverExecutablePath: driverExecutablePath, options: chromeOptions);

            for (int i = 0; i < repeat; i++)
            {
                try
                {
                    webdriver.GoToUrl(shopDict["CategoryPageWithPageNumber"]
                        .Replace("{i}", "1")
                        .Replace("{categoryId}", categoryId));

                    var pagesInCategory = Enumerable.Range(1, int.Parse(webdriver.FindElement(By.XPath(
                        shopDict["PagesInCategoryXpath"])).Text));
                    webdriver.Quit();
                    return pagesInCategory;
                }
                catch
                {
                    webdriver.Quit();
                }
            }

            return new List<int>();
        }

        private static string FindElementText(this IWebElement driver, string xPath)
        {
            try
            {
                return driver.FindElement(By.XPath(xPath)).Text;
            }
            catch
            {
                return "";
            }
        }

        private static ChromeOptions NewChromeOptions()
        {
            var ChOp = new ChromeOptions();
            ChOp.AddArguments("--headless=new");
            return ChOp;
        }
    }
}