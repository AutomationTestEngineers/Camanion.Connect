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
        private IWebElement OutcomeSubType = null, AnimalCondition = null, OutcomeFee = null, comments=null, nextButton=null, behaviornote=null;

        [FindsBy(How = How.Name, Using = "IntakeSubType")]
        private IWebElement intakeSubType = null;


        public void EnterRTODetails()
        {
            OutcomeSubType.SelectByIndex(driver, 1);
            AnimalCondition.SelectByIndex(driver, 1);
            OutcomeFee.SelectByIndex(driver, 1);
            comments.ClearAndPaste(FakeData.Word, driver);
            nextButton.ClickCustom(driver);
        }

        public void EnterMedicalHistory()
        {
            ClickWithLoop(nextButton.GetLocator());
        }

        public DonationPage EnterBahviorCare()
        {
            behaviornote.ClearAndPaste("Behavior Notes", driver);
            ClickWithLoop(nextButton.GetLocator());
            return new DonationPage(driver);
        }

    }
}
