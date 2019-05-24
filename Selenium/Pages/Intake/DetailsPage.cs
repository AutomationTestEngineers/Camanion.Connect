using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake
{
    public class DetailsPage : BasePage
    {
        public DetailsPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement site = null, subSite = null, location = null, subLocation = null, intakeDetailsnote = null, intakeServicenote=null;

        [FindsBy(How = How.Name, Using = "IntakeSubType")]
        private IWebElement intakeSubType = null;

        [FindsBy(How = How.Name, Using = "AnimalCondition")]
        private IWebElement animalCondition = null;

        [FindsBy(How = How.Id, Using = "intakeDetails-save")]
        private IWebElement saveNotes = null;

        [FindsBy(How = How.XPath, Using = "//button[@data-ng-click='vm.done()']")]
        private IWebElement saveAndClose = null;

        [FindsBy(How = How.XPath, Using = "//button[@data-ng-click='vm.gotoNextStep()']")]
        private IWebElement goNext = null;


        public HomePage EnterDetailsInfo()
        {
            intakeSubType.SelectByIndex(driver);
            animalCondition.SelectByIndex(driver);
            if (FindBy(By.Name("OriginalSource"), 1, true)!=null)
                FindBy(By.Name("OriginalSource"), 1, true).SelectByIndex(driver, 1);
            site.SelectByIndex(driver);
            subSite.SelectByIndex(driver);
            location.SelectByIndex(driver);
            subLocation.SelectByIndex(driver);
            intakeDetailsnote.SendKeysWrapper("intake Detailsnote", driver);
            saveNotes.ClickCustom(driver);
            Signature();            
            saveAndClose.ClickCustom(driver);
            Sleep(3500);
            return new HomePage(driver);
        }

        public HomePage EnterDetails(string type)
        {
            if(type.Trim()== "Service")
            {
                site.SelectByIndex(driver);
                subSite.SelectByIndex(driver);
                location.SelectByIndex(driver);
                subLocation.SelectByIndex(driver);
                intakeServicenote.SendKeysWrapper("intake Service note", driver);
                FindBy(By.Id("intakeService-save")).ClickCustom(driver);
                Signature();
                goNext.ClickCustom(driver);
                Sleep(3000);
            }
            return new HomePage(driver);
        }
    }
}
