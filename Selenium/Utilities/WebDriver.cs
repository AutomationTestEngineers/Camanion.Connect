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

            switch (Config.Browser.ToLower())
            {
                case "chrome":
                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--disable-infobars");
                    options.AddArguments("--disable-extensions");
                    options.AddArguments("test-type");
                    options.AddArguments("no-sandbox");
                    options.AddArguments("--disable-plugins");
                    options.AddArguments("--enable-precise-memory-info");
                    options.AddArguments("--disable-popup-blocking");
                    options.AddArguments("--disable-default-apps");
                    options.AddArguments("test-type=browser");
                    options.AddAdditionalCapability("useAutomationExtension", false);
                    driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(Int16.Parse(Config.BrowserLoad)));
                    break;

                default:
                    throw new ArgumentException($"Browser Option {Config.Browser} Is Not Valid - Use Chrome, Edge or IE Instead");
            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Config.Url);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Int16.Parse(Config.PageLoad));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(Int16.Parse(Config.ImplicitWait));
            return driver;
        }
    }
}