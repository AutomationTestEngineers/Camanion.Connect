using OpenQA.Selenium;
using Selenium.Pages.Outcome;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages
{
    public class AdministrationPage : BasePage
    {
        public AdministrationPage(IWebDriver driver): base(driver) { }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Outcomes')]")]
        private IWebElement outComes = null, outcomeSearch=null;

        public OutcomeSearchPage DeleteOutComes()
        {
            outComes.ClickCustom(driver);return new OutcomeSearchPage(driver);

        }
    }
}
