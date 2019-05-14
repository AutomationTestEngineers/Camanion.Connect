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


        public DetailsPage EnterMedicalInfo(string index=null)
        {
            Sleep(1000);
            Wait(ExpectedConditions.ElementExists(givenIntake.GetLocator()));
            if (index!=null)
                FindBy(By.XPath($"(//ng-form[@name='medicalForm']/div[1]//input[@type='checkbox']/../i[1])[{index}]"), 1, true).ClickCustom(driver);
            else
                givenIntake.ClickCustom(driver);

            if (index == null)  // Scedule Date Present
            {
                Sleep(300);
                if (FindBy(By.XPath("//ng-form[@name='medicalForm']/div[1]//input[contains(@name,'ScheduleDate') or contains(@name,'ScheduledDate')]"), 1, true) != null)
                    FindBy(By.XPath("//ng-form[@name='medicalForm']/div[1]//input[contains(@name,'ScheduleDate') or contains(@name,'ScheduledDate')]")).SendKeysWrapper(DateTime.Today.ToShortDateString(), driver);
                Sleep(500);
            }            
            intakeMedicalVaccinationnote.SendKeys("Medical Notes");
            Sleep(100);
            saveNotes_Medical.ClickCustom(driver);
            nextBtn.ClickCustom(driver);
            return new DetailsPage(driver);
        }
    }
}
