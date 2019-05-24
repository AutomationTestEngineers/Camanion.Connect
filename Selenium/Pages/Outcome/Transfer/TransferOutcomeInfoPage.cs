using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome.Transfer
{
    public class TransferOutcomeInfoPage : BasePage
    {
        public TransferOutcomeInfoPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement animalcondition = null, transferreason=null, nextButton=null;


        public void EnterTransferOutcomeInfo()
        {
            animalcondition.SelectByIndex(driver, 1);
            transferreason.SelectByIndex(driver, 1);
            nextButton.ClickCustom(driver);
        }
    }
}
