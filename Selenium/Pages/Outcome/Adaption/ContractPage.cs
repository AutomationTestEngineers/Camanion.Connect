using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome.Adaption
{
    public class ContractPage :BasePage
    {
        public ContractPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement nextButton = null;


        public DonationPage ClickNext()
        {
            Signature(true);
            ClickWithLoop(nextButton.GetLocator());
            return new DonationPage(driver);
        }
    }
}
