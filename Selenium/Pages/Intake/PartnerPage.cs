using OpenQA.Selenium;
using Selenium.Pages;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    public class PartnerPage : BasePage
    {
        public PartnerPage(IWebDriver driver): base(driver) { }

        [FindsBy]
        private IWebElement searchButton=null;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'Search')][contains(@ng-model,'vm.')]")]
        private IWebElement search = null;


        public string GetheaderName()
        {
            return FindBy(By.XPath("//*[@id='content']/div[3]/div[1]//div//h1")).GetText(driver);
        }
        public void SearchPartner(string partner)
        {
            search.SendText(partner, driver);
            searchButton.ClickCustom(driver);
        }
    }
}
