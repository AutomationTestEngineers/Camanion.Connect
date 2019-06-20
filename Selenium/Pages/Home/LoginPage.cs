using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Selenium.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        //[FindsBy]
        //private IWebElement email = null, password = null;


        [FindsBy(How = How.XPath, Using = "//*[@name='email']")]
        private IWebElement email = null;

        [FindsBy(How = How.XPath, Using = "//*[@name='password']")]
        private IWebElement password = null;

        [FindsBy(How = How.XPath, Using = "//div[2]/form/div[5]/div/button")]
        private IWebElement logInButton = null;
        

        public HomePage Login()
        {            
            Wait(ExpectedConditions.ElementToBeClickable(this.email.GetLocator()), 5);
            this.email.SendKeysWrapper(Parameter.Get<string>("Email"),driver);
            this.password.SendKeysWrapper(Encoding.UTF8.GetString(Convert.FromBase64String(Parameter.Get<string>("Password"))), driver);
            Wait(ExpectedConditions.ElementToBeClickable(logInButton.GetLocator()),5);
            logInButton.ClickCustom(driver);
            return new HomePage(driver);
        }
    }
}
