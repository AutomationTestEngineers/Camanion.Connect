using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    public static class WebDriverExtension
    {
        public static IWebElement Find(this IWebDriver driver,By by, int timeout = 5)
        {
            try
            {                
                return FindBy(driver, by, timeout);
            }
            catch(StaleElementReferenceException e)
            {
                return FindBy(driver, by, timeout);
            }           
        } 
        private static IWebElement FindBy(IWebDriver driver, By by,int timeout)
        {
            driver.Wait(ExpectedConditions.ElementIsVisible(by), timeout);
            return driver.FindElement(by);
        }
        public static void Wait<TResult>(this IWebDriver driver,Func<IWebDriver, TResult> condition, int seconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }
    }
}
