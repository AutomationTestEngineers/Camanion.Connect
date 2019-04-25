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
        private IWebElement keywordSearch=null, searchButton=null;


        public string GetheaderName()
        {
            return FindBy(By.XPath("//*[@id='content']/div[3]/div[1]/progress-step/div//h1")).GetText(driver);
        }
        public void SearchPartner(string partner)
        {
            keywordSearch.SendText(partner, driver);
            searchButton.ClickCustom(driver);
        }
    }
}
