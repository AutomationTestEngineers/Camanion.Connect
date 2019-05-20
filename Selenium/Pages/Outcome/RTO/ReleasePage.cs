using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome
{
    public class ReleasePage : BasePage
    {
        public ReleasePage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement closeButton = null;

        [FindsBy(How = How.CssSelector, Using = "button[ng-click='vm.close()']")]
        private IWebElement close = null;


        public void Release()
        {
            driver.Popup();
            close.ClickCustom(driver);
        }

        public void CloseButton()
        {
            driver.Popup();
            close.ClickCustom(driver);
        }
    }
}
