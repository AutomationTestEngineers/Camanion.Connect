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

            switch (Parameter.Get<string>("Browser").ToLower())
            {
                case "chrome":
                    {
                        ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--disable-infobars");
                        options.AddArguments("--disable-extensions");
                        options.AddArguments("test-type");
                        options.AddArguments("no-sandbox");
                        options.AddArguments("disable-plugins");
                        options.AddArguments("--enable-precise-memory-info");
                        options.AddArguments("--disable-popup-blocking");
                        options.AddArguments("--disable-default-apps");
                        options.AddArguments("test-type=browser");
                        driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(120));                        
                        break;
                    }
                
                default:
                    throw new ArgumentException($"Browser Option {Parameter.Get<string>("Browser")} Is Not Valid - Use Chrome, Edge or IE Instead");

            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Parameter.Get<string>("SiteUrl"));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
            return driver;
        }
    }
}