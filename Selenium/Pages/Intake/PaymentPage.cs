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
            firstName=null, LastName=null, address1=null, zipCode=null, city=null, checkAmount=null, checkRegisterName=null, PaymentOutsideOfConnectAmount=null;

        [FindsBy(How = How.XPath, Using = "//*[@id='configure-filter']//i")]
        private IWebElement personalInfoChkBox = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='radio-blue display-inline']//i[1]")]
        private IList<IWebElement> paymentMethods = null;

        [FindsBy(How = How.XPath, Using = "//button[@id='nextButton']")]
        private IWebElement submitPayment = null;

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.completePayment()']")]
        private IWebElement completePayment = null;
        

        public AnimalPage EnterPayment(string method)
        {
            AnimalFee0.SelectByIndex(driver, 1);
            nextButton.ClickCustom(driver);
            if (method.ToLower().Contains("cash"))
                CashMethod(1);
            else if (method.ToLower().Contains("creditcard"))
                CreditCardMethod();
            else if (method.ToLower().Contains("check"))
                this.Check(2);
            else
                this.PaymentOutsideOfConnect(3);
            completePayment.ClickCustom(driver);
            return new AnimalPage(driver);
        }

        public void CreditCardMethod()
        {
            if(FindBy(By.XPath("//*[@id='configure-filter']//i"),2,true).Displayed)
                personalInfoChkBox.ClickCustom(driver);
            else
                EnterBillingInfo();
            CardNumber.SendKeysWrapper("4111111111111111", driver);
            month.SelectByIndex(driver, 5);
            year.SelectByIndex(driver, 4);
            ccv.SendKeysWrapper("123", driver);
            Signature(false);
            submitPayment.ClickCustom(driver);
            driver.Popup();
        }

        public void CashMethod(int method)
        {
            paymentMethods[method].ClickCustom(driver);
            amountReceived.SendKeysWrapper("100", driver);
            cashRegisterName.SelectByIndex(driver, 1);
            submitPayment.ClickCustom(driver);
            driver.Popup();
        }        

        public void Check(int method)
        {
            paymentMethods[method].ClickCustom(driver);
            checkAmount.SendKeysWrapper("100",driver);
            checkRegisterName.SelectByIndex(driver, 1);
            submitPayment.ClickCustom(driver);
            driver.Popup();
        }

        public void PaymentOutsideOfConnect(int method)
        {
            paymentMethods[method].ClickCustom(driver);
            PaymentOutsideOfConnectAmount.SendKeysWrapper("100", driver);
            submitPayment.ClickCustom(driver);
            driver.Popup();
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
