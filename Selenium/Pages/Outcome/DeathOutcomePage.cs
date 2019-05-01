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
        private IWebElement causeOfDeath = null, Deathnote=null, nextButton=null, closeButton=null;


        public void EnterDeathDetails()
        {
            Sleep(3000);
            causeOfDeath.SelectDropDown(driver,Parameter.Get<string>("CauseOfDeath"));
            Deathnote.SendKeysWrapper(FakeData.Word, driver);
            nextButton.ClickCustom(driver);
            closeButton.ClickCustom(driver);
        }
    }
}
