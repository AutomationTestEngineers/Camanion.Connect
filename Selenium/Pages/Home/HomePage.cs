using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.XPath, Using = "//header[@id='shellHeader']/nav/div/div[2]/div/ul/li/a")]
        private IWebElement email = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/input")]
        private IWebElement searchInput = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/span/button")]
        private IWebElement searchButton = null;        

        [FindsBy(How = How.XPath, Using = "//div[@id='mainmenu']//button/i[@class='icon icon-setting']")]
        private IWebElement gearIcon = null;

        [FindsBy(How = How.XPath, Using = "//li[2]//a[contains(text(),'Change Shelter Selection')]")]
        private IWebElement changeShelter = null;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'OK')]")]
        private IWebElement ok = null;

        [FindsBy(How = How.XPath, Using = "//tbody/tr/td[3]")]
        private IList<IWebElement> searchList = null;        

        [FindsBy]
        private IWebElement newIntake=null;
        
        public void ChangeShelter(string shelterName)
        {
            gearIcon.ClickCustom(driver);
            changeShelter.ClickCustom(driver);
            FindBy(By.XPath($"//strong[contains(text(),'{shelterName}')]"),10).ClickCustom(driver);
            ok.ClickCustom(driver);
        }        

        public string GetUserName()
        {
            return email.GetText(driver);
        }
        public void EnterSearch(string text)
        {
            searchInput.SendText(text,driver);
            searchButton.ClickCustom(driver);
        }
        public List<string> getSearchList()
        {
            return searchList.GetText(driver);
        }
        public PersonPage NewAddIntake(string intakeType)
        {
            newIntake.ClickCustom(driver);

            string xpath = string.Empty;
            if (intakeType.ToLower().Contains("publicstray"))
                xpath = "Public Stray";
            else if (intakeType.ToLower().Contains("surrender"))
                xpath = "Surrender";
            else if (intakeType.ToLower().Contains("return"))
                xpath = "Return";
            else if (intakeType.ToLower().Contains("trasfer"))
                xpath = "Transfer";
            else if (intakeType.ToLower().Contains("animalcontrol"))
                xpath = "Animal Control";
            else if (intakeType.ToLower().Contains("service"))
                xpath = "Service";

            FindBy(By.XPath($"//div/a[contains(text(),'{xpath}')]")).ClickCustom(driver);

            return new PersonPage(driver);
        }
    }
}
