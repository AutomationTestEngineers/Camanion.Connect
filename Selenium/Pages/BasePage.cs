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

        public IWebElement FindBy(By by,int i=5,bool exist = false)
        {
            try
            {
                if (exist) { Sleep(i * 1000); return driver.FindElement(by); }                    
                Wait(ExpectedConditions.ElementExists(by),i);
                return driver.FindElement(by);
            }
            catch(StaleElementReferenceException e)
            {
                return FindBy(by);
            }
            catch(NoSuchElementException ex)
            {
                return null;
            }
        }

        public void ScreenBusy(int timeout = 120)
        {
            Thread.Sleep(1000);
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='modal-backdrop fade in']")),timeout);
            Thread.Sleep(1000);
        }

        public void Signature()
        {
            try
            {
                var element = FindBy(By.XPath("//signature-pad/div/div[2]/div[1]/a"), 2, true);
                if (element.Displayed)
                {
                    FindBy(By.XPath("//div[@class='checkbox-blue']/label/i")).ClickCustom(driver);
                    FindBy(By.XPath("//signature-pad/div/div[2]/div[1]/a")).ClickCustom(driver);
                    var signature = FindBy(By.XPath("//signature-pad/div/div[1]/canvas"));
                    actions.MoveToElement(signature).Click().MoveByOffset(200, 80).Click().MoveByOffset(200, 500)
                        .DoubleClick().Build().Perform();
                    FindBy(By.XPath("//signature-pad/div/div[2]/div[2]/a")).ClickCustom(driver);
                    Thread.Sleep(200);
                    ScreenBusy();
                }
            } catch { }
        }

        public void Sleep(int timeout = 1000)
        {
            Thread.Sleep(timeout);
        }        
    }
}
