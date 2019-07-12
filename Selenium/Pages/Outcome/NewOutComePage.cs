using OpenQA.Selenium;
using Selenium.Pages.Outcome.Adaption;
using Selenium.Pages.Outcome.Transfer;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome
{
    public class NewOutComePage : BasePage
    {
        public NewOutComePage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement closeButton = null;

        public void SelectOutcome(string outCome)
        {            
            FindBy(By.XPath($"//a[@id='outcomeType.Name'][normalize-space()='{outCome}']")).ClickCustom(driver); //Click Corresponding Outcome
            
            switch (outCome)
            {                
                case "Death":                    
                    (new DeathOutcomePage(driver)).EnterDeathDetails();
                    break;
                case "Euthanasia":                    
                    (new DetailsPage(driver)).EnterDetailsEuthanasia();
                    break;
                case "Transfer":
                    Transfer();
                    break;
                case "Return to Owner":
                    RTO();
                    break;
                case "Adoption":
                    Adaption();
                    break;
                case "Service":
                    var service = new ServiceOutcomePage(driver);
                    service.EnterOutcomeService();
                    service.EnterDonation();
                    service.EnterPayment();
                    break;
            }
        }

        public void RTO()
        {
            var details = (new PersonPage(driver)).SearchPartner("k");
            details.EnterRTODetails();
            details.EnterMedicalHistory();
            var donation = details.EnterBahviorCare();
            var payment = donation.EnterDonation();
            payment.PaymentBreakDown();
            payment.PaymentMethod();
            var release = payment.PaymentSummary();
            release.Release();
        }

        public void Transfer()
        {
            var person = (new PersonPage(driver));
            person.SearchPartner("k");
            person.NextButton();
            driver.Popup(false);
            var trasfer = new TransferOutcomeInfoPage(driver);
            trasfer.EnterTransferOutcomeInfo();
            ClickWithLoop(closeButton.GetLocator());
        }

        public void Adaption()
        {
            var person = (new PersonPage(driver));
            person.SearchPartner("k");
            var review = new ReviewCareHistoryPage(driver);
            var campanion = review.EnterReviewHistory();
            var contract = campanion.ClickNext();
            var donation = contract.ClickNext();
            var payment = donation.EnterDonation();
            payment.PaymentBreakDown();
            payment.PaymentMethod();
            var release = payment.PaymentSummaryAdaption();
            release.CloseButton();

            //ClickWithLoop(By.Id("next-step")); //Next button Review Care History
            //Sleep(2000);
            //Signature();  // Companion Project Page
            //ClickWithLoop(By.Id("nextButton")); //Next button Companion Project Page
            //Sleep(2000);
            //Signature();  // Contract Page
            //ClickWithLoop(By.Id("nextButton")); //Next button Contract Page
            //var payment = (new DonationPage(driver)).EnterDonation(); // Donation
            //payment.PaymentBreakDown();
            //payment.PaymentMethod();
            //Signature();
            //var release = payment.PaymentSummary();
            //ClickWithLoop(By.Id("nextButton")); //Next button Payment Pag            
            //release.Release();
        }
    }
}
