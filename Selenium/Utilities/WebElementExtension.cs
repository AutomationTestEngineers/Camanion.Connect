using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Selenium
{
    public static class WebElementExtensions
    {
        public static void ScreenBusy(this IWebElement element,IWebDriver driver, int timeout = 60)
        {
            Thread.Sleep(100);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='modal-backdrop fade in']")));
        }
        public static void ScreenBusy(IWebDriver driver, int timeout = 60)
        {
            Thread.Sleep(100);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='modal-backdrop fade in']")));
        }

        public static By GetLocator(this IWebElement element)
        {
            var elementProxy = RemotingServices.GetRealProxy(element);
            var bysFromElement = (IReadOnlyList<object>)elementProxy
                .GetType()
                .GetProperty("Bys", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                .GetValue(elementProxy);
            return (By)bysFromElement[0];
        }

        public static By GetLocator(this IList<IWebElement> element)
        {
            var elementProxy = RemotingServices.GetRealProxy(element);
            var bysFromElement = (IReadOnlyList<object>)elementProxy
                .GetType()
                .GetProperty("Bys", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                .GetValue(elementProxy);
            return (By)bysFromElement[0];
        }

        public static void SelectDropDown(this IWebElement element, IWebDriver driver, string option = null, bool js = false)
        {
            ScreenBusy(driver);
            element.HighlightElement(driver);
            if (!js)
            {                 
                bool selected = false;
                var options = element.FindElements(By.TagName("option"));
                if (!string.IsNullOrEmpty(option))
                    foreach (var a in options)
                    {
                        if (a.Text == option)
                        {
                            a.ClickCustom(driver);
                            selected = true;
                            break;
                        }
                    }
                else
                {
                    options[1].Click();
                    selected = true;
                }
                if (!selected)
                    throw new Exception($"[Error] : Selecting combobox value [{option}] to  element [{element}] was unsuccessfull");
            }
            else
            {
                JavaScriptExecutor(string.Format(JSOperator.DropDown, option), element);
            }
        }

        public static void SendText(this IWebElement element, string text,IWebDriver driver, bool js = false)
        {
            try
            {
                ScreenBusy(driver);
                element.HighlightElement(driver);
                if (!js)
                {
                    element.ClearManual();
                    Thread.Sleep(100);
                    element.PasteFromClipboard(text);
                }
                else
                {
                    JavaScriptExecutor(JSOperator.Clear, element);
                    JavaScriptExecutor(string.Format(JSOperator.SetValue, text), element);
                }
                ScreenBusy(driver);
            }
            catch (Exception e)
            {
                if (HandlePopUp(driver))
                    element.SendText(text,driver);
                else
                    throw new Exception("[Error] : While Sending Text & [Message] : [" + e.Message+"]");
            }
        }

        public static string GetText(this IWebElement element,IWebDriver driver)
        {
            ScreenBusy(driver);
            Thread.Sleep(500);
            return element.Text;
        }

        public static List<string> GetText(this IList<IWebElement> element,IWebDriver driver)
        {
            List<string> list=null;
            try
            {
                Thread.Sleep(500);
                ScreenBusy(driver);
                Thread.Sleep(500);
                list = element.Select(e => e.Text).ToList();
            }
            catch (Exception e)
            {
                if (HandlePopUp(driver))
                    element.GetText(driver);
                else
                    throw new Exception("[Error] : While  Handling Popup & [Message] :" + e.Message);
            }
            return list;
        }

        public static void SendTextAndSelect(this IWebElement element, string text, IWebDriver driver,bool js = false)
        {
            try
            {
                ScreenBusy(driver);
                element.HighlightElement(driver);
                if (!js)
                {
                    element.Click();
                    element.ClearManual();
                    Thread.Sleep(100);
                    for (int i = 0; i < text.Length; i++)
                        element.SendKeys(text[i].ToString());
                }
                else
                {
                    JavaScriptExecutor(JSOperator.Clear, element);
                    JavaScriptExecutor(string.Format(JSOperator.SetValue, text), element);
                }
            }
            catch (Exception e)
            {
                if (HandlePopUp(driver))
                    element.SendText(text, driver);
                else
                    throw new Exception("[Error] : While Sending Text & [Message] : [" + e.Message + "]");
            }
        }
        public static void ClickCustom(this IWebElement element,IWebDriver driver, bool js = false)
        {
            try
            {
                element.ScreenBusy(driver);
                element.HighlightElement(driver);
                if (!js)
                    element.Click();
                else
                    JavaScriptExecutor(JSOperator.Click, element);                
            }
            catch (Exception e)
            {
                if (HandlePopUp(driver))
                    element.ClickCustom(driver);
                else
                    throw new Exception("[Error] : While Click & [Message] :" + e.Message);
            }

        }

        public static void ClearManual(this IWebElement element)
        {
            element.SendKeys(OpenQA.Selenium.Keys.Control + "a");
            element.SendKeys(OpenQA.Selenium.Keys.Delete);
        }

        public static void PasteFromClipboard(this IWebElement element, string textToCopy)
        {
            Thread thread = new Thread(() => Clipboard.SetText(textToCopy));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
            element.SendKeys(OpenQA.Selenium.Keys.Control + "v");
        }


        public static IWebDriver GetWrappedDriver(this IWebElement element)
        {
            IWebDriver instance = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    Thread.Sleep(100);
                    instance = (element as IWrapsDriver ?? (IWrapsDriver)((IWrapsElement)element).WrappedElement).WrappedDriver;
                    break;
                }
                catch { continue; }
            }
            if (instance != null)
                return instance;
            else
                throw new Exception("[Info : WebDriver instace is not created]");
        }

        public static void ScrollElement(this IWebElement element, IWebDriver driver)
        {
            Thread.Sleep(20);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(20);
        }        

        public static bool HandlePopUp(IWebDriver driver)
        {
            try
            {
                string xpath = "//button[contains(text(),'OK')] | //button[contains(text(),'Yes')]";
                Thread.Sleep(300);
                ScreenBusy(driver);
                var e = driver.FindElement(By.XPath(xpath));
                Wait(ExpectedConditions.ElementToBeClickable(e),driver,5);
                e.ClickCustom(driver);
                Thread.Sleep(300);
                ScreenBusy(driver);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private static void JavaScriptExecutor(string pattern, IWebElement element)
        {
            var js = element.GetWrappedDriver() as IJavaScriptExecutor;
            js.ExecuteScript(pattern, element);
        }

        public static void Wait<TResult>(Func<IWebDriver, TResult> condition, IWebDriver driver, int seconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }

        public static void HighlightElement(this IWebElement element,IWebDriver driver)
        {
            for (int i = 0; i < 2; i++)
            {
                (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", element, "border: 3px solid blue;");
                (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", element, "border: 0px solid blue;");
            }

        }

        public static IJavaScriptExecutor Js(this IWebElement element)
        {
            Thread.Sleep(50);
            return element.GetWrappedDriver() as IJavaScriptExecutor;
        }

        private static class JSOperator
        {
            public static string Click { get { return "arguments[0].click();"; } }
            public static string Clear { get { return "arguments[0].value = '';"; } }
            public static string SetValue { get { return "arguments[0].value = '{0}';"; } }
            public static string IsDisplayed { get { return "if(parseInt(arguments[0].offsetHeight) > 0 && parseInt(arguments[0].offsetWidth) > 0) return true; return false;"; } }
            public static string ValidateAttribute { get { return "return arguments[0].getAttribute('{0}');"; } }
            public static string ScrollToElement { get { return "arguments[0].scrollIntoView(true);"; } }
            public static string DropDown { get { return "var length = arguments[0].options.length;  for (var i=0; i<length; i++){{  if (arguments[0].options[i].text == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}"; } }

        }

    }
}
