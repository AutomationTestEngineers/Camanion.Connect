using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium.Pages
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

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/span/label[1]/span//i[1]")]
        private IWebElement activeOnly = null;

        [FindsBy(How = How.XPath, Using = "//div[@id='mainmenu']//button/i[@class='icon icon-setting']")]
        private IWebElement gearIcon = null;

        [FindsBy(How = How.XPath, Using = "//li[2]//a[contains(text(),'Change Shelter Selection')]")]
        private IWebElement changeShelter = null;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'OK')]")]
        private IWebElement ok = null;

        [FindsBy(How = How.XPath, Using = "//tbody/tr/td[3]")]
        private IList<IWebElement> searchList = null;
        
        public void ChangeShelter(string shelterName)
        {
            gearIcon.ClickCustom();
            changeShelter.ClickCustom();
            FindBy(By.XPath($"//strong[contains(text(),'{shelterName}')]")).ClickCustom();
            ok.ClickCustom();
        }
        public string GetUserName()
        {
            return email.Text;
        }
        public void EnterSearch(string text)
        {
            searchInput.SendText(text);
            searchButton.ClickCustom();
        }
        public List<string> getSearchList()
        {
            ScreenBusy();
            Thread.Sleep(500);
            return searchList.Select(e => e.Text).ToList();
        }
    }
}
