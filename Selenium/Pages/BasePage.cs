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
        protected int minTimeOut = 10;
       

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            actions = new Actions(driver);  
            PageFactory.InitElements(this.driver, this);
            ScreenBusy();
        }

        public WebDriverWait WebDriverWait
        { get
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(minTimeOut));
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);
                wait.IgnoreExceptionTypes(typeof(Exception));
                return wait;
            }            
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
                if (exist) { Sleep(i * 500); return driver.FindElement(by); }                    
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
            Thread.Sleep(300);
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@role='progressbar']/*/child::*[1] | //div[@class='modal-backdrop fade in']/div/child::*']")), timeout);            
            Thread.Sleep(100);
        }

        public void Signature(bool chkbox =true)
        {
            try
            {
                var element = FindBy(By.XPath("//signature-pad/div/div[2]/div[1]/a"), 1, true);
                if (element.Displayed)
                {

                    //if (chkbox)
                    //{
                    //    Wait(ExpectedConditions.ElementExists(By.XPath("(//label[@type='checkbox']/span/span/child::*[1]) | (//div[@class='checkbox-blue']/label/i)")), 10);
                    //    FindBy(By.XPath("(//label[@type='checkbox']/span/span/child::*[1]) | (//div[@class='checkbox-blue']/label/i)")).ClickCustom(driver);
                    //}
                    try
                    {
                        if (chkbox)
                            Wait(ExpectedConditions.ElementExists(By.XPath("(//label[@type='checkbox']/span/span/child::*[1]) | (//div[@class='checkbox-blue']/label/i)")),10);                        
                    }
                    catch { }
                    if (FindBy(By.XPath("(//label[@type='checkbox']/span/span/child::*[1]) | (//div[@class='checkbox-blue']/label/i)"),1,true)!=null)
                    {
                        FindBy(By.XPath("(//label[@type='checkbox']/span/span/child::*[1]) | (//div[@class='checkbox-blue']/label/i)")).ClickCustom(driver);
                    }    

                    FindBy(By.XPath("//signature-pad/div/div[2]/div[1]/a")).ClickCustom(driver);
                    var signature = FindBy(By.XPath("//signature-pad/div/div[1]/canvas"));
                    actions.MoveToElement(signature).ClickAndHold().MoveByOffset(165, 15).MoveByOffset(185, 15)
                        .Release().Build().Perform();
                    FindBy(By.XPath("//signature-pad/div/div[2]/div[2]/a")).ClickCustom(driver);                    
                    ScreenBusy();
                }
            } catch { }
        }

        public void Sleep(int timeout = 1000)
        {
            Thread.Sleep(timeout);
        }        

        public void ClickWithLoop(By by,int retry=3)
        {
            bool found = false;
            for(int i = 0; i < retry; i++)
            {
                try
                {
                    ScreenBusy(30);
                    driver.FindElement(by).HighlightElement(driver);
                    WebDriverWait.Until(ExpectedConditions.ElementToBeClickable(by));
                    driver.FindElement(by).Click();
                    found = true;
                    ScreenBusy(60);
                    return;
                }
                catch (Exception e){}
            }
            if(!found)
            {
                throw new Exception("Unable To Perform Click On Element : "+by);
            }
        }
    }
}
