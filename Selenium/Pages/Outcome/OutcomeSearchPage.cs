using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome
{
    public class OutcomeSearchPage : BasePage
    {
        public OutcomeSearchPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement outcomeSearch = null, searchButton=null;

        public void SearchOutcome()
        {
            outcomeSearch.SendKeysWrapper(Parameter.Get<string>("AnimalName"),driver);
            searchButton.ClickCustom(driver);
        }
    }
}
