

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumUndetectedChromeDriver;

namespace tests.ParserTests
{
    public class Experiment
    {
        [Test]
        public void test()
        {
            var driverExecutablePath = $@"C:\Users\andrew\.nuget\packages\selenium.webdriver.chromedriver\124.0.6367.15500\driver\win32\chromedriver.exe";
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless=new");

            var driver1 = UndetectedChromeDriver.Create(
                driverExecutablePath: driverExecutablePath, options: chromeOptions);
            driver1.Url = "https://magnit.ru/catalog/?categoryId=4834";
            // теперь надо сделать так чтоб поиск мог выдать ноль для какого то из значений 
            var a = driver1.FindElement(By.XPath(" .//*[@class='new-card-product__']/div[1]")).Text;
            driver1.Quit();





        }
    }
}