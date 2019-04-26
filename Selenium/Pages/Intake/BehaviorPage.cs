using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake
{
    public class BehaviorPage : BasePage
    {
        public BehaviorPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'Search')][contains(@ng-model,'vm.')]")]
        private IWebElement search = null;
    }
}
