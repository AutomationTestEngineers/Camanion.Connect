using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome
{
    public class ServiceOutcomePage :BasePage
    {
        public ServiceOutcomePage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement inputvalue = null, nextButton=null, amountReceived=null, cashRegisterName=null;

        [FindsBy(How = How.Name, Using = "OutcomeFee")]
        private IWebElement OutcomeFee = null;

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.gotoNextStep()']")]
        private IWebElement next = null;

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.nextStep()']")]
        private IWebElement next_Payment = null;

        [FindsBy(How = How.XPath, Using = "//*[@id='configure-filter']//i")]
        private IWebElement personalInfoChkBox = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='radio-blue display-inline']//i[1]")]
        private IList<IWebElement> paymentMethods = null;

        [FindsBy(How = How.XPath, Using = "//*[@id='outcomepayment']/div[4]/div[2]/button/any[2]")]
        private IWebElement submitPayment = null;

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.completePayment()']")]
        private IWebElement completePayment = null;

        [FindsBy(How = How.CssSelector, Using = "button[ng-click='vm.close()']")]
        private IWebElement close = null;


        public void EnterOutcomeService()
        {
            int[] array = new int[] { 2, 3, 5 };
            foreach (int value in array)
            {
                Wait(ExpectedConditions.ElementToBeClickable(OutcomeFee.GetLocator()), 5);
                OutcomeFee.SelectByIndex(driver,value);
                Sleep(200);
            }
            next.ClickCustom(driver);
        }

        public void EnterDonation()
        {
            Wait(ExpectedConditions.ElementExists(inputvalue.GetLocator()));
            inputvalue.SendKeysWrapper("500",driver);
            nextButton.ClickCustom(driver);
            next_Payment.ClickCustom(driver);
        }

        public void EnterPayment(int method=1)
        {
            personalInfoChkBox.ClickCustom(driver);
            if (method == 1) // default Cash Method 
                CashMethod();

            completePayment.ClickCustom(driver);
            close.ClickCustom(driver);
        }

        public void CashMethod()
        {
            paymentMethods[1].ClickCustom(driver);
            amountReceived.SendKeysWrapper("1000", driver);
            cashRegisterName.SelectByIndex(driver, 1);
            submitPayment.ClickCustom(driver);
            driver.Popup();
        }
    }
}
