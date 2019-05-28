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
        private IWebElement outcomeSearch = null, searchButton=null, dateFrom=null;

        [FindsBy(How = How.XPath, Using = "(//div[@class='row'])[1]/div[1]/div/div[1]")]
        private IWebElement searchType = null;

        [FindsBy(How = How.XPath, Using = "(//tr/td[last()]/a[2])[1]")]
        private IWebElement x_Btn = null;


        public void SearchOutcome()
        {
            dateFrom.SendKeys(Keys.Tab);
            searchType.ClickCustom(driver);
            var searchTypeInput = FindBy(By.XPath("(//div[@class='row'])[1]/div[1]/div/div[1]/following-sibling::input[1]"), 1, true);
            searchTypeInput.SendKeys("Animal Name");
            searchTypeInput.SendKeys(Keys.Enter);
            outcomeSearch.SendKeysWrapper(Parameter.Get<string>("AnimalName"), driver);
            searchButton.ClickCustom(driver);
            
        }

        public void DeleteOutcome()
        {
            x_Btn.ClickCustom(driver);
            driver.Popup();
            ScreenBusy();
            Sleep(100);
        }
    }
}
