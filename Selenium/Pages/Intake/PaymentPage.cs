using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake
{
    public class PaymentPage:BasePage
    {
        public PaymentPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement AnimalFee0 = null, nextButton=null, amountReceived = null, cashRegisterName = null, CardNumber=null, month=null, year=null, ccv=null,
            firstName=null, LastName=null, address1=null, zipCode=null, city=null;

        [FindsBy(How = How.XPath, Using = "//*[@id='configure-filter']//i")]
        private IWebElement personalInfoChkBox = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='radio-blue display-inline']//i[1]")]
        private IList<IWebElement> paymentMethods = null;

        [FindsBy(How = How.XPath, Using = "//button[@id='nextButton']")]
        private IWebElement submitPayment = null;

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.completePayment()']")]
        private IWebElement completePayment = null;

        [FindsBy(How = How.CssSelector, Using = "button[ng-click='vm.close()']")]
        private IWebElement close = null;

        

        public AnimalPage EnterPayment(int method = 0)
        {
            AnimalFee0.SelectByIndex(driver, 3);
            nextButton.ClickCustom(driver);
            personalInfoChkBox.ClickCustom(driver);
            CreditCardMethod();
            completePayment.ClickCustom(driver);
            return new AnimalPage(driver);
        }

        public void CreditCardMethod()
        {
            CardNumber.SendKeysWrapper("4111111111111111", driver);
            month.SelectByIndex(driver, 5);
            year.SelectByIndex(driver, 4);
            ccv.SendKeysWrapper("123", driver);
            Signature(false);
            submitPayment.ClickCustom(driver);
            driver.Popup();
        }

        public void CashMethod(int method = 0)
        {
            if(method!=0)
                paymentMethods[method].ClickCustom(driver);
            amountReceived.SendKeysWrapper("1000", driver);
            cashRegisterName.SelectByIndex(driver, 1);
            submitPayment.ClickCustom(driver);
            driver.Popup();
        }

        public AnimalPage EnterPayment()
        {
            AnimalFee0.SelectByIndex(driver, 1);
            nextButton.ClickCustom(driver);
            //EnterBillingInfo();
            CashMethod(1);
            completePayment.ClickCustom(driver);
            return new AnimalPage(driver);
        }

        public void EnterBillingInfo()
        {
            firstName.SendKeysWrapper(FakeData.FirstName,driver);
            LastName.SendKeysWrapper(FakeData.LastName, driver);
            address1.SendKeysWrapper(FakeData.StreetAddress, driver);
            zipCode.SendKeysWrapper(FakeData.Zip, driver);
            city.SendKeysWrapper(FakeData.City, driver);
        }
    }
}
