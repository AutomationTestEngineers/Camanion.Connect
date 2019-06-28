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
            Sleep(700);
            Wait(ExpectedConditions.ElementExists(givenIntake.GetLocator()));

            if (!string.IsNullOrEmpty(index)) // Index Based Intake
                FindBy(By.XPath($"(//ng-form[@name='medicalForm']/div[1]//input[@type='checkbox']/../i[1])[{index}]"), 1, true).ClickCustom(driver);
            else
                givenIntake.ClickCustom(driver);

            if (!string.IsNullOrEmpty(index))  // If Scedule Date Present
            {
                ScreenBusy();
                Sleep(100);
                IWebElement schedule = FindBy(By.XPath("//ng-form[@name='medicalForm']/div[1]//input[contains(@name,'ScheduleDate') or contains(@name,'ScheduledDate')]"));
                if (!string.IsNullOrEmpty(index) && schedule.Displayed)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Sleep(500);
                        if (FindBy(By.XPath("//label/text()[contains(normalize-space(),'for a specific date.')]/preceding-sibling::i[1]")).GetCssValue("color") != "rgba(176, 176, 176, 1)")
                            break;
                    }
                    Sleep(500);
                    schedule.SendKeysWrapper(DateTime.Today.ToShortDateString(), driver);
                    schedule.SendKeys(Keys.Enter);
                }   
            }
            intakeMedicalVaccinationnote.SendKeysWrapper("Medical Notes",driver);            
            saveNotes_Medical.ClickCustom(driver);
            nextBtn.ClickCustom(driver);
            return new DetailsPage(driver);
        }
    }
}
