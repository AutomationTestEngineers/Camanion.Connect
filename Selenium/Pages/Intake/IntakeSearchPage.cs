using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake
{
    public class IntakeSearchPage : BasePage
    {
        public IntakeSearchPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement searchField = null, searchButton = null, fromDate = null;

        [FindsBy(How = How.XPath, Using = "(//div[@class='row'])[2]/div[1]/div/div[1] | (//div[@class='row'])[6]/div[1]/div/div[1]")]
        private IWebElement searchType = null;

        [FindsBy(How = How.XPath, Using = "(//tr/td[last()]/a[2])[1]")]
        private IWebElement x_Btn = null;

        [FindsBy(How = How.CssSelector, Using = "div[class='toast-message']")]
        private IWebElement toast = null;

        

        public void SearchIntake(string name)
        {
            fromDate.SendKeys(Keys.Tab);
            searchType.ClickCustom(driver);
            var searchTypeInput = FindBy(By.XPath("((//div[@class='row'])[6] |(//div[@class='row'])[2])/div[1]/div/div[1]/following-sibling::input[1]"), 1, true);
            searchTypeInput.SendKeys("Animal Name");
            searchTypeInput.SendKeys(Keys.Enter);
            searchField.SendKeysWrapper(name, driver);
            searchButton.ClickCustom(driver);
        }

        public void DeleteIntake()
        {
            x_Btn.ClickCustom(driver);
            driver.Popup(true);
            ScreenBusy();
        } 
        
        public string GetToastMessage()
        {
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='modal-backdrop fade in']")), 10);
            toast.HighlightElement(driver);
            return toast.GetText(driver,true);
        }

        public void SearchIntake_Specific(string p0)
        {
            fromDate.SendKeysWrapper(DateTime.Today.AddDays(-40).ToShortDateString(), driver);
            fromDate.SendKeys(Keys.Tab);
            searchType.ClickCustom(driver);
            var searchTypeInput = FindBy(By.XPath("((//div[@class='row'])[6] |(//div[@class='row'])[2])/div[1]/div/div[1]/following-sibling::input[1]"), 1, true);
            searchTypeInput.SendKeys("Animal Name");
            searchTypeInput.SendKeys(Keys.Enter);
            searchField.SendKeysWrapper(p0, driver);
            searchButton.ClickCustom(driver);
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    x_Btn.ClickCustom(driver);
                    driver.Popup(true);
                    ScreenBusy();                   
                }catch { break;}                
            }
        }
    }
}
