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
    public class DetailsPage : BasePage
    {
        public DetailsPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement OutcomeSubType = null, AnimalCondition = null, OutcomeFee = null, comments=null, nextButton=null, behaviornote=null,
            euthanasiaReason=null, performedBy=null, euthanasiaSubtance=null, euthanasiaDosage=null, method=null, route=null, Deathnote=null,
            closeButton=null;

        [FindsBy(How = How.Name, Using = "IntakeSubType")]
        private IWebElement intakeSubType = null;

        [FindsBy(How = How.Name, Using = "euthanasiaMeasurement")]
        private IWebElement euthanasiaMeasurement = null;


        public void EnterRTODetails()
        {
            OutcomeSubType.SelectByIndex(driver, 1);
            AnimalCondition.SelectByIndex(driver, 1);
            OutcomeFee.SelectByIndex(driver, 1);
            comments.SendKeysWrapper(FakeData.Word, driver);
            nextButton.ClickCustom(driver);
        }

        public void EnterMedicalHistory()
        {
            ClickWithLoop(nextButton.GetLocator());
        }

        public DonationPage EnterBahviorCare()
        {
            behaviornote.SendKeysWrapper("Behavior Notes", driver);
            ClickWithLoop(nextButton.GetLocator());
            return new DonationPage(driver);
        }

        public void EnterDetailsEuthanasia()
        {
            euthanasiaReason.SelectByIndex(driver, 2);
            performedBy.SelectByIndex(driver, 1);
            euthanasiaSubtance.SelectByIndex(driver, 1);
            euthanasiaDosage.SendKeysWrapper("10", driver);
            euthanasiaMeasurement.SelectByIndex(driver, 1);
            method.SelectByIndex(driver, 1);
            route.SelectByIndex(driver, 1);
            Deathnote.SendKeysWrapper("Death Comment Notes For Testing", driver);
            ClickWithLoop(nextButton.GetLocator());
            ClickWithLoop(closeButton.GetLocator());            
        }
    }
}
