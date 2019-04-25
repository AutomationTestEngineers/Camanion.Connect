using Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Selenium
{
    public class WebDriver
    {
        public virtual IWebDriver InitDriver()
        {
            IWebDriver driver;

            switch (Config.Browser)
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
                    throw new ArgumentException($"Browser Option {Config.Browser} Is Not Valid - Use Chrome, Edge or IE Instead");

            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Config.Url);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
            return driver;
        }
    }
}