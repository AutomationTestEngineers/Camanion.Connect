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
    public class PersonPage : BasePage
    {
        public PersonPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement searchButton = null;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'Search')][contains(@ng-model,'vm.')]")]
        private IWebElement search = null;

        [FindsBy(How = How.XPath, Using = "(//tbody/tr/td/a)[1]")]
        private IWebElement select = null;

        [FindsBy(How = How.XPath, Using = "//button[@id='save-person' or @id='nextButton']")]
        private IWebElement nextButton = null;


        public DetailsPage SearchPartner(string partner)
        {
            search.SendKeysWrapper(partner, driver);
            searchButton.ClickCustom(driver);
            Wait(ExpectedConditions.ElementExists(select.GetLocator()));
            try // To Handle Multiple Select Hyperlinks
            {
                select.HighlightElement(driver);
                select.Click();
            }
            catch {
                FindBy(By.XPath("(//tbody/tr/td/a[@class='cursor-pointer ng-scope ng-isolate-scope'])[1]"),1,true).Click();
            }            
            //nextButton.ClickCustom(driver);
            return new DetailsPage(driver);
        }

        public void NextButton()
        {
            try
            {
                Wait(ExpectedConditions.ElementToBeClickable(nextButton.GetLocator()),5);
                ClickWithLoop(nextButton.GetLocator());
            }
            catch
            {
                //ClickWithLoop(cancelButton.GetLocator());
                //driver.Popup();
            }
            
        }
    }
}
