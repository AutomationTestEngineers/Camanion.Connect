using Configuration;
using OpenQA.Selenium;
using Selenium.Pages;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selenium
{
    public class PersonPage : BasePage
    {
        public PersonPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement searchButton = null, continueButton=null;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'Search')][contains(@ng-model,'vm.')]")]
        private IWebElement search = null;

        [FindsBy(How = How.XPath, Using = "(//tbody/tr/td/a)[1]")]
        private IWebElement select = null;

        [FindsBy(How = How.XPath, Using = "//button[@id='save-person' or @id='nextButton']")]
        private IWebElement nextButton = null;


        public AnimalPage SearchPartner(string partner)
        {
            search.SendKeysWrapper(partner, driver);
            searchButton.ClickCustom(driver);
            Sleep(700);
            Wait(ExpectedConditions.ElementExists(select.GetLocator()));
            select.ClickCustom(driver);
            nextButton.ClickCustom(driver);
            return new AnimalPage(driver);
        }

        public AnimalPage WithNoPerson()
        {
            continueButton.ClickCustom(driver);
            return new AnimalPage(driver);
        }
    }
}
