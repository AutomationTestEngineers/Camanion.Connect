using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;

namespace Selenium.Pages
{
    public abstract class BasePage
    {
        protected Actions actions;
        protected IWebDriver driver;
        public BasePage(IWebDriver driver, int pageLodTimeOut = 60, int elemtTimeOut = 30)
        {
            actions = new Actions(driver);
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
            ScreenBusy();
        }
        public void Wait<TResult>(Func<IWebDriver, TResult> condition, int seconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }
        public IWebElement FindBy(By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch(StaleElementReferenceException e)
            {
                return FindBy(by);
            }
        }
        public void ScreenBusy(int timeout = 60)
        {
            Thread.Sleep(1000);
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='modal-backdrop fade in']")),timeout);
            Thread.Sleep(1000);
        }
    }
}
