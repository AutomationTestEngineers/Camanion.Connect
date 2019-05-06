using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake
{
    public class MedicalPage : BasePage
    {
        public MedicalPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement intakeMedicalVaccinationnote = null;

        [FindsBy(How = How.CssSelector, Using = "#intakeMedicalVaccination-save")]
        private IWebElement saveNotes_Medical = null;

        [FindsBy(How = How.XPath, Using = "//input[@id='VACC_GIVEN_0']/following-sibling::i[1]")]
        private IWebElement givenIntake = null;

        [FindsBy(How = How.XPath, Using = "//button[@data-ng-click='vm.gotoNextStep()']")]
        private IWebElement nextBtn = null;


        public DetailsPage EnterMedicalInfo()
        {
            Sleep(1000);
            Wait(ExpectedConditions.ElementExists(givenIntake.GetLocator()));
            givenIntake.ClickCustom(driver);
            Sleep(2000);
            intakeMedicalVaccinationnote.SendKeys("MedicalNotes");
            Sleep(100);
            saveNotes_Medical.ClickCustom(driver);
            nextBtn.ClickCustom(driver);
            return new DetailsPage(driver);
        }
    }
}
