using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Selenium
{
    public class WebDriver
    {
        public virtual IWebDriver InitDriver(string browser,string Url)
        {
            IWebDriver driver;

            switch (browser)
            {
                case "chrome":
                    {
                        ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--disable-infobars");
                        options.AddArguments("--disable-extensions");
                        driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(120));                        
                        break;
                    }
                
                default:
                    throw new ArgumentException($"Browser Option {browser} Is Not Valid - Use Chrome, Edge or IE Instead");

            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            return driver;
        }
    }
}