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
    public class PaymentPage : BasePage
    {
        public PaymentPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement CardNumber = null, month = null, year=null, ccv=null;

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.nextStep()']")]
        private IWebElement next = null;

        [FindsBy(How = How.XPath, Using = "//*[@id='configure-filter']//i")]
        private IWebElement personalInfoChkBox = null;

        //[FindsBy(How = How.XPath, Using = "//div[@class='radio-blue display-inline']//i[1]")]
        //private IList<IWebElement> paymentMethods = null;

        [FindsBy(How = How.XPath, Using = "//button[@id='nextButton']")]
        private IWebElement submitPayment = null;

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.completePayment()']")]
        private IWebElement completePayment = null;



        public void PaymentBreakDown()
        {
            ScreenBusy();
            ClickWithLoop(next.GetLocator());
        }

        public void PaymentMethod()
        {
            personalInfoChkBox.ClickCustom(driver);
            CreditCardMethod();
        }

        public void CreditCardMethod()
        {
            CardNumber.SendKeysWrapper("4111111111111111", driver);
            month.SelectByIndex(driver, 5);
            year.SelectByIndex(driver, 4);
            ccv.SendKeysWrapper("123", driver);
            //if (FindBy(By.XPath("(//label[@type='checkbox']/span/span/child::*[1]) | (//div[@class='checkbox-blue']/label/i)"),2,true)!=null)
            //    FindBy(By.XPath("(//label[@type='checkbox']/span/span/child::*[1]) | (//div[@class='checkbox-blue']/label/i)")).ClickCustom(driver);
            Signature(false);
            ClickWithLoop(next.GetLocator());
            driver.Popup();
        }
        

        public ReleasePage PaymentSummary()
        {
            ClickWithLoop(completePayment.GetLocator());
            return new ReleasePage(driver);
        }

        public ReleasePage PaymentSummaryAdaption()
        {
            ClickWithLoop(completePayment.GetLocator());
            Signature(true);
            submitPayment.ClickCustom(driver);
            return new ReleasePage(driver);
        }
    }
}
