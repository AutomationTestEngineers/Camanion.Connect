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
        private IWebElement microchipNumber = null, issuer=null, animalAltered=null, btnSave=null, btnClose=null, NewOutcome=null,
            MedicalCare=null, BehavioralCare=null, note=null, addNote=null, submitButton=null, clinicName=null, clinicPhone=null, PetIDNumber_ = null, petIdType_=null, animalStatus=null, btnAddPetId = null;

        //[FindsBy(How = How.CssSelector, Using = "span[data-target='#collapseHold']")]
        //private IWebElement animalCurrentHolds = null;

        [FindsBy(How = How.XPath, Using = "//button[normalize-space()='Release Hold']")]
        private IWebElement releaseHoldBtn = null;

        [FindsBy(How = How.XPath, Using = "(//button[@id='ViewHoldHistory'])[1]")]
        private IWebElement ViewHoldHistory = null;

        [FindsBy(How = How.XPath, Using = "//form[@id='viewAnimalProfileForm']//div[@id='animalDetail']/div/label/span/span")]
        private IWebElement animalDetails = null;        

        [FindsBy(How = How.XPath, Using = "//form[@id='viewAnimalProfileForm']//div[@id='currenthold']/div/label/span/span")]
        private IWebElement animalCurrentHolds = null;

        [FindsBy(How = How.TagName, Using = "textarea")]
        private IWebElement comments = null;

        [FindsBy(How = How.XPath, Using = "//input[@id='inFoster']/..//span")]
        private IWebElement urgentChkBox = null;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Submit')]")]
        private IWebElement submit = null;

        [FindsBy(How = How.XPath, Using = "//*[@id='rabiesVaccine']/../span")]
        private IWebElement rabiesVaccine = null;

        [FindsBy(How = How.XPath, Using = "//div[@data-ng-repeat='item in vm.observations']//i[1]")]
        private IList<IWebElement> abservations = null;


        [FindsBy(How = How.CssSelector, Using = "i[class='fa fa-square-o col-xs-2']")]
        private IList<IWebElement> symptoms = null;


        public void EnterMicroChipDetails()
        {
            Sleep(500);
            Parameter.Add<string>("MicroChipNumber",FakeData.Number(1,10000));
            microchipNumber.SendKeysWrapper(Parameter.Get<string>("MicroChipNumber"),driver);
            issuer.SelectByIndex(driver);            
        }

        public void EnterAnimalDetails()
        {
            Sleep(100);
            driver.ScrollPage(0, 1000);
            Sleep(500);
            actions.MoveToElement(animalDetails).Click().Build().Perform();
            Sleep(1000);
            if (FindBy(By.XPath("(//div[7]//*[@id='headingAction']/label/span)[1][@class='expandable collapsed']"), 2, true) != null)
                animalDetails.ClickCustom(driver);

            animalAltered.SelectByIndex(driver, 2);
            btnSave.ClickCustom(driver);
        }

        public void ReleaseHolds()
        {            
            Sleep(200);
            ScreenBusy();
            driver.ScrollPage(0, 1000);
            Sleep(20);
            Wait(ExpectedConditions.PresenceOfAllElementsLocatedBy(animalCurrentHolds.GetLocator()),10);
            bool state = true;
            while (state)
            {
                try
                {
                    driver.FindElement(animalCurrentHolds.GetLocator()).Click();
                    state = false;
                }
                catch(Exception e) { state = true; }
            }
            //actions.MoveToElement(FindBy(animalCurrentHolds.GetLocator())).Click().Build().Perform();
            Sleep(300);
            if (FindBy(By.XPath("(//div[6]//*[@id='headingAction']/label/span)[1][@class='expandable ng-scope collapsed']"), 1, true) != null)
                animalCurrentHolds.ClickCustom(driver);

            ViewHoldHistory.ClickCustom(driver);
            for(int i = 0; i < 20; i++)
            {
                try
                {
                    Sleep(300);
                    FindBy(By.XPath("(//tbody/tr/td/a)[1]")).ClickCustom(driver);
                    releaseHoldBtn.ClickCustom(driver);
                }
                catch { Console.WriteLine(" <<<< Holds Released #"+i+" >>>>"); break; }
            }
            btnClose.ClickCustom(driver);
        }        

        public NewOutComePage ClickNewOutcome()
        {
            Sleep(500);
            NewOutcome.ClickCustom(driver);
            return new NewOutComePage(driver);
        }

        public void MedicalExam()
        {
            MedicalCare.ClickCustom(driver);
            int[] index = new int[] { 14, 17, 20 };
            //actions.MoveToElement(FindBy(By.XPath("//*[@id='content']/div[3]/section/section/div/div[2]/div/div/div/div[1]/div/div/div/div[2]/div[" + index[i] + "]//i[1]"))).Click().Build().Perform();
            for (int i = 0; i < index.Length; i++) // Select Animal Symptoms
                actions.MoveToElement(symptoms[index[i]]).Click().Build().Perform();
            comments.SendKeysWrapper("Medical Exam Comments", driver);
            urgentChkBox.ClickCustom(driver);
            submit.ClickCustom(driver);
        }

        public void BehaviorExam()
        {
            BehavioralCare.ClickCustom(driver);
            for (int i = 1; i <= 4; i++)
                abservations[i].ClickCustom(driver);
            note.SendKeysWrapper("Behavior Exam Notes", driver);
            addNote.ClickCustom(driver);
            submitButton.ClickCustom(driver);
        }

        public void EnterAnimalRabiesVaccineDetails()
        {
            rabiesVaccine.ClickCustom(driver);
            clinicName.SendKeysWrapper(FakeData.ClinicName, driver);
            clinicPhone.SendKeysWrapper(FakeData.PhoneNumber, driver);
            PetIDNumber_.SendKeysWrapper(FakeData.Number(1, 1300000), driver);
            petIdType_.SelectByIndex(driver, 11);
            btnAddPetId.ClickCustom(driver);
            //animalStatus.SelectDropDown(driver, "Available");
            driver.ScrollPage(0, -250);
            Sleep(300);
            ReleaseHolds();
            Sleep(3000);
        }
    }
}
