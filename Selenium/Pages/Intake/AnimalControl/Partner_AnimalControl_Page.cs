using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake.AnimalControl
{
    public class PartnerPageAnimalControl : BasePage
    {
        public PartnerPageAnimalControl(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement searchButton = null, nextButton = null;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'Search')][contains(@ng-model,'vm.')]")]
        private IWebElement search = null;

        [FindsBy(How = How.XPath, Using = "(//tbody/tr/td/a)[1]")]
        private IWebElement select = null;


        public AnimalPageAnimalControl SearchPartner(string partner)
        {            
            search.SendText(partner, driver);
            searchButton.ClickCustom(driver);
            Sleep(1000);
            select.ClickCustom(driver);
            nextButton.ClickCustom(driver);
            return new AnimalPageAnimalControl(driver);
        }
    }
}
