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

        [FindsBy(How = How.XPath, Using = "(//div[@class='row'])[1]/div[1]/div/div[1] | (//div[@class='row'])[5]/div[1]/div/div[1]")]
        private IWebElement searchType = null;

        [FindsBy(How = How.XPath, Using = "(//tr/td[last()]/a[2])[1]")]
        private IWebElement x_Btn = null;


        public void SearchOutcome(string name)
        {
            dateFrom.SendKeys(Keys.Tab);
            searchType.ClickCustom(driver);
            var searchTypeInput = FindBy(By.XPath("((//div[@class='row'])[1]|(//div[@class='row'])[5])/div[1]/div/div[1]/following-sibling::input[1]"), 1, true);
            searchTypeInput.SendKeys("Animal Name");
            searchTypeInput.SendKeys(Keys.Enter);
            outcomeSearch.SendKeysWrapper(name, driver);
            searchButton.ClickCustom(driver);
            
        }

        public void DeleteOutcome()
        {
            x_Btn.ClickCustom(driver);
            driver.Popup();
            ScreenBusy();
            Sleep(100);
        }

        public void SearchOutcome_Specific(string p0)
        {
            dateFrom.SendKeysWrapper(DateTime.Today.AddDays(-40).ToShortDateString(), driver);
            dateFrom.SendKeys(Keys.Enter);
            dateFrom.SendKeys(Keys.Tab);
            searchType.ClickCustom(driver);
            var searchTypeInput = FindBy(By.XPath("((//div[@class='row'])[1]|(//div[@class='row'])[5])/div[1]/div/div[1]/following-sibling::input[1]"), 1, true);
            searchTypeInput.SendKeys("Animal Name");
            searchTypeInput.SendKeys(Keys.Enter);
            outcomeSearch.SendKeysWrapper(p0, driver);
            searchButton.ClickCustom(driver);
            //var a = driver.FindElements(By.XPath("(//tr/td[last()]/a[2])"));
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    x_Btn.ClickCustom(driver);
                    driver.Popup();
                    ScreenBusy();
                    Sleep(100);
                }
                catch { break; }
            }
        }
    }
}
