using Configuration;
using OpenQA.Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
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

        [FindsBy]
        private IWebElement newIntake = null, editAnimal=null;

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

        [FindsBy(How = How.XPath, Using = "//li[2]//a[contains(text(),'Admin')]")]
        private IWebElement admin = null;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'OK')]")]
        private IWebElement ok = null;

        [FindsBy(How = How.XPath, Using = "//tbody/tr/td[3]")]
        private IList<IWebElement> searchList = null;

        [FindsBy(How = How.XPath, Using = "(//div[@id='mainmenu']//span[@class='input-group-btn']//i)[2]")]
        private IWebElement activeOnly = null;

        [FindsBy(How = How.CssSelector, Using = "#mainmenu > form > div > div > div > div > select > option:nth-child(1)")]
        private IWebElement searchDropDown = null;



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

        public NewIntakePage NewAddIntake()
        {
            newIntake.ClickCustom(driver);
            return new NewIntakePage(driver);
        }

        public void SearchAnimal(string searchName=null)
        {
            Sleep(1000);
            if (searchName == null)
                searchName = Parameter.Get<string>("AnimalName");
            activeOnly.ClickCustom(driver);
            searchDropDown.ClickCustom(driver);
            searchInput.SendKeysWrapper(searchName, driver);
            searchButton.ClickCustom(driver);
        }

        public string GetAnimalName()
        {
            Sleep(500);
            return FindBy(By.XPath("//table[@class='table table-custom']//td[3]")).Text;            
        }

        public void ClickPencilIcon()
        {
            FindBy(By.XPath("(//span[@class='glyphicon glyphicon-pencil'])[1]")).ClickCustom(driver);
            Sleep(2000);
        }
        public ProfilePage EditAnimal()
        {
            editAnimal.ClickCustom(driver);
            return new ProfilePage(driver);
        }
        
        public void DeleteRecentOutCome()
        {
            gearIcon.ClickCustom(driver);
            admin.ClickCustom(driver);
        }
    }
}
