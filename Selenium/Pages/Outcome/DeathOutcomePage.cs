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
    public class DeathOutcomePage : BasePage
    {
        public DeathOutcomePage(IWebDriver driver): base(driver) { }

        [FindsBy]
        private IWebElement causeOfDeath = null, Deathnote=null, nextButton=null, closeButton=null,
            euthanasiaReason=null, performedBy=null, euthanasiaSubtance=null, euthanasiaDosage=null, method=null, route=null;

        [FindsBy(How = How.CssSelector, Using = "select[name='euthanasiaMeasurement']")]
        private IWebElement euthanasiaMeasurement = null;

        //Partner Details
        [FindsBy]
        private IWebElement searchButton = null, keywordSearch = null;




        public void EnterDeathDetails()
        {
            Sleep(3000);
            causeOfDeath.SelectByIndex(driver,2);
            Deathnote.SendKeysWrapper(FakeData.Word, driver);
            nextButton.ClickCustom(driver);
            closeButton.ClickCustom(driver);
        }

        public void EnterDeathEuthanasia()
        {
            euthanasiaReason.SelectByIndex(driver,1);
            performedBy.SelectByIndex(driver, 1);
            euthanasiaSubtance.SelectByIndex(driver, 1);
            euthanasiaDosage.SendKeysWrapper("1", driver);
            euthanasiaMeasurement.SelectByIndex(driver, 2);
            method.SelectByIndex(driver, 2);
            route.SelectByIndex(driver, 2);
            Deathnote.SendKeysWrapper(FakeData.Word, driver);
            nextButton.ClickCustom(driver);
            closeButton.ClickCustom(driver);
        }

        public void EnterDeathTransfer()
        {
            keywordSearch.SendKeysWrapper("k", driver);
            searchButton.ClickCustom(driver);
            FindBy(By.XPath("(//tr/td[last()]/a)[1]"),10).ClickCustom(driver);
            nextButton.ClickCustom(driver);
            nextButton.ClickCustom(driver);
            closeButton.ClickCustom(driver);
        }
    }
}
