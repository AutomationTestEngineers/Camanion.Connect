using OpenQA.Selenium;
using Selenium.Pages.Intake;
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

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Outcomes']")]
        private IWebElement outComes = null;

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Intakes']")]
        private IWebElement intakes = null;

        public OutcomeSearchPage ClickOutcomes()
        {
            outComes.ClickCustom(driver);return new OutcomeSearchPage(driver);
        }

        public IntakeSearchPage ClickIntakes()
        {
            intakes.ClickCustom(driver);  return new IntakeSearchPage(driver);
        }
       
    }
}
