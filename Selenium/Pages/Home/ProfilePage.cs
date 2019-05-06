using Configuration;
using OpenQA.Selenium;
using Selenium.Pages.Outcome;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages
{
    public class ProfilePage : BasePage
    {
        public ProfilePage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement microchipNumber = null, issuer=null, animalAltered=null, btnSave=null,
             btnClose=null, NewOutcome=null;

        [FindsBy(How = How.CssSelector, Using = "span[data-target='#collapseHold']")]
        private IWebElement animalCurrentHolds = null;

        [FindsBy(How = How.XPath, Using = "//form[@name='animalHold']/div[6]/div/div[1]/button")]
        private IWebElement releaseHoldBtn = null;

        [FindsBy(How = How.XPath, Using = "(//button[@id='ViewHoldHistory'])[1]")]
        private IWebElement ViewHoldHistory = null;

        [FindsBy(How = How.XPath, Using = "(//div[7]//*[@id='headingAction']/label/span)[1]")]
        private IWebElement animalDetails = null;


        public void EnterMicroChipDetails()
        {
            Parameter.Add<string>("MicroChipNumber",FakeData.Number(1,10000));
            microchipNumber.SendKeysWrapper(Parameter.Get<string>("MicroChipNumber"),driver);
            issuer.SelectByIndex(driver);            
        }

        public void EnterAnimalDetails()
        {
            Sleep(1000);
            actions.MoveToElement(animalDetails).Click().Build().Perform();
            if (FindBy(By.XPath("(//div[7]//*[@id='headingAction']/label/span)[1][@class='expandable collapsed']"), 1, true) != null)
                animalDetails.ClickCustom(driver);

            animalDetails.ClickCustom(driver);
            animalAltered.SelectByIndex(driver, 2);
            btnSave.ClickCustom(driver);
        }

        public void ReleaseHolds()
        {            
            Sleep(2000);
            ScreenBusy();
            var ele = FindBy(By.CssSelector("span[data-target='#collapseHold']"),3,true);
            actions.MoveToElement(ele).Click().Build().Perform();
            if(FindBy(By.CssSelector("div[id='collapseHold'][class='panel-collapse collapse']"), 1, true)!=null)
                animalCurrentHolds.ClickCustom(driver);

            ViewHoldHistory.ClickCustom(driver);
            for(int i = 0; i < 20; i++)
            {
                try
                {
                    Sleep(500);
                    FindBy(By.XPath("(//tbody/tr/td/a)[1]")).ClickCustom(driver);
                    releaseHoldBtn.ClickCustom(driver);
                }
                catch { break; }
            }
            btnClose.ClickCustom(driver);
        }

        public NewOutComePage ClickNewOutcome()
        {
            Sleep(1000);
            NewOutcome.ClickCustom(driver);
            return new NewOutComePage(driver);
        }
    }
}
