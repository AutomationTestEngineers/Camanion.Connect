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
        public static void ScreenBusy(this IWebElement element,int timeout = 60)
        {
            Thread.Sleep(100);
            var wait = new WebDriverWait(element.GetWrappedDriver(),TimeSpan.FromSeconds(timeout));
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

        public static void SelectDropDown(this IWebElement element, string option = null, bool js = false)
        {
            element.ScreenBusy();
            element.HighlightElement();
            if (!js)
            {
                bool selected = false;
                var options = element.FindElements(By.TagName("option"));
                if (!string.IsNullOrEmpty(option))
                    foreach (var a in options)
                    {
                        if (a.Text == option)
                        {
                            a.ClickCustom();
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
                    throw new Exception($"Selecting combobox value [{option}] to  element [{element}] was unsuccessfull");
            }
            else
            {
                JavaScriptExecutor(string.Format(JSOperator.DropDown, option), element);
            }
            element.ScreenBusy();
        }

        public static void SendText(this IWebElement element, string text, bool js = false)
        {
            element.ScreenBusy();
            element.HighlightElement();
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
            element.ScreenBusy();
        }

        public static void ClickCustom(this IWebElement element, bool js = false)
        {
            element.ScreenBusy();
            element.HighlightElement();
            if (!js)
                element.Click();
            else
                JavaScriptExecutor(JSOperator.Click, element);
            element.ScreenBusy();
        }

        public static void CheckBox(this IWebElement element, bool isChecked = true)
        {
            element.ScreenBusy();
            element.HighlightElement();
            if (element.Selected != isChecked)
                element.Click();
            else
                element.Click();
            element.ScreenBusy();
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

        /// <summary>
        /// To get the driver instance from element
        /// </summary>
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

        #region Java Executor
        private static void JavaScriptExecutor(string pattern, IWebElement element)
        {          
            var js = element.GetWrappedDriver() as IJavaScriptExecutor;
            js.ExecuteScript(pattern, element);
        }

        public static void HighlightElement(this IWebElement element)
        {
            for (int i = 0; i < 2; i++)
            {
                element.Js().ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", element, "border: 3px solid blue;");
                element.Js().ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", element, "border: 0px solid blue;");
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

        #endregion

    }
}
