using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace Selenium
{
    public class BasePage
    {

        protected Actions actions;
        protected IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            actions = new Actions(driver);  
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
        public IWebElement FindBy(By by,int i=5)
        {
            try
            {
                Wait(ExpectedConditions.ElementExists(by),i);
                return driver.FindElement(by);
            }
            catch(StaleElementReferenceException e)
            {
                return FindBy(by);
            }
        }
        public void ScreenBusy(int timeout = 90)
        {
            Thread.Sleep(1000);
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='modal-backdrop fade in']")),timeout);
            Thread.Sleep(1000);
        }
    }
}
