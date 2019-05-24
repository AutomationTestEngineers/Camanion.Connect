using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium
{
    public static class WebDriverExtension
    {
        public static IWebElement Find(this IWebDriver driver,By by, int timeout = 5)
        {
            try
            {
                IWebElement element = null;
                for (int i = 0;i<= timeout;i++)
                {
                    Thread.Sleep(300);
                    element= driver.FindElement(by);
                    break;
                }
                return element;
            }
            catch(StaleElementReferenceException e)
            {
                Thread.Sleep(500);
                return driver.FindElement(by);
            }
            catch (NoSuchElementException ex)
            {
                return null;
            }
        } 

        public static void Popup(this IWebDriver driver,bool clickYes=true)
        {
            if(driver.Find(By.XPath("//div[@class='modal-content']"), 10)!=null)
            {
                if (clickYes)
                    FindBy(driver,By.XPath("//div[@class='modal-content']//button[normalize-space()='OK' or normalize-space()='Yes' or normalize-space()='Ok' or normalize-space()='YES']"), 5).ClickCustom(driver);
                else
                    FindBy(driver,By.XPath("//div[@class='modal-content']//button[normalize-space()='No' or normalize-space()='NO']"), 5).ClickCustom(driver);
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

        public static void TakeScreenshot(this IWebDriver driver, string fileNameBase)
        {
            try
            {
                var folderName = "testresults" + DateTime.Now.ToString("yyyyMMdd");
                var artifactDirectory = Path.Combine("C:\\evidence\\screenshots", folderName);
                if (!Directory.Exists(artifactDirectory)) Directory.CreateDirectory(artifactDirectory);
                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;
                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();
                    string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.jpg");
                    Console.WriteLine($"[Screen Shot Path] {screenshotFilePath}");
                    var screenshotBase64 = screenshot.AsBase64EncodedString;
                    SaveByteArrayAsImage(screenshotFilePath, screenshotBase64);
                    //Console.WriteLine($"[IMG] {screenshotBase64}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("[ERROR]: Error while taking screenshot: {0}", ex);
            }
        }

        private static void SaveByteArrayAsImage(string screenshotFilePath, string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            image.Save(screenshotFilePath, ImageFormat.Jpeg);
            //Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
        }

        public static void GetScreenShot(this IWebDriver driver,string testName)
        {
            string fileNameBase = string.Format("Error_{0}_{1}", testName, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            driver.TakeScreenshot(fileNameBase);
        }

        public static void ScrollPage(this IWebDriver driver,int x,int y)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy({x},{y})");
        }
    }
}
