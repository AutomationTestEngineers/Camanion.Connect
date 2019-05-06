using Configuration;
using OpenQA.Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
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
        private IWebElement newIntake = null, editAnimal=null, searchValue=null;

        [FindsBy(How = How.XPath, Using = "//a[@class='QuickAdd-button']")]
        private IWebElement addBtn = null;

        [FindsBy(How = How.XPath, Using = "//header[@id='shellHeader']/nav/div/div[2]/div/ul/li/a")]
        private IWebElement email = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/input")]
        private IWebElement searchInput = null;

        //[FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/span/button")]
        //private IWebElement searchButton = null;

        [FindsBy(How = How.XPath, Using = "//button[@class='SearchBar-button']")]
        private IWebElement searchButton = null;

        //[FindsBy(How = How.XPath, Using = "//div[@id='mainmenu']//button/i[@class='icon icon-setting']")]
        //private IWebElement gearIcon = null;  

        [FindsBy(How = How.XPath, Using = "(//*[@role='presentation'])[last()]")]
        private IWebElement gearIcon = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='Account-container']/div")]
        private IList<IWebElement> selection = null;

        [FindsBy(How = How.XPath, Using = "//li[2]//a[contains(text(),'Change Shelter Selection')]")]
        private IWebElement changeShelter = null;

        //[FindsBy(How = How.XPath, Using = "//li[2]//a[contains(text(),'Admin')]")]
        //private IWebElement admin = null;

        [FindsBy(How = How.LinkText, Using = "/admin")]
        private IWebElement admin = null;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'OK')]")]
        private IWebElement ok = null;

        [FindsBy(How = How.XPath, Using = "//tbody/tr/td[3]")]
        private IList<IWebElement> searchList = null;

        //[FindsBy(How = How.XPath, Using = "(//div[@id='mainmenu']//span[@class='input-group-btn']//i)[2]")]
        //private IWebElement activeOnly = null;

        [FindsBy(How = How.XPath, Using = "(//div[@class='SearchBar-checkbox'])[1]//span[1]")]
        private IWebElement activeOnly = null; 

        //[FindsBy(How = How.CssSelector, Using = "#mainmenu > form > div > div > div > div > select > option:nth-child(1)")]
        //private IWebElement searchDropDown = null;



        public void ChangeShelter(string shelterName)
        {
            gearIcon.ClickCustom(driver);
            Sleep(500);
            selection.Where(t => t.Text.Contains("Change Shelter Location")).FirstOrDefault().ClickCustom(driver);

            //gearIcon.ClickCustom(driver);
            //changeShelter.ClickCustom(driver);
            //FindBy(By.XPath($"//strong[contains(text(),'{shelterName}')]"),10).ClickCustom(driver);
            //ok.ClickCustom(driver);
        }        

        public string GetUserName()
        {
            return email.GetText(driver);
        }

        public void EnterSearch(string text)
        {
            searchInput.SendKeysWrapper(text,driver);
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

            ClickActiveOnly();
            FindBy(By.XPath("//div[@id='searchType']/div")).ClickCustom(driver);
            FindBy(By.XPath("(//div[text()='Animal Name'])[last()]")).ClickCustom(driver);

            //searchDropDown.ClickCustom(driver);
            searchValue.SendKeysWrapper(searchName, driver);
            searchButton.ClickCustom(driver);
        }

        public void ClickActiveOnly()
        {
            searchValue.ClickCustom(driver);
            activeOnly.ClickCustom(driver);
        }

        public string GetAnimalName()
        {
            Sleep(500);
            return FindBy(By.XPath("//table[@class='table table-custom']//td[3]")).Text;            
        }

        public void ClickPencilIcon()
        {
            FindBy(By.XPath("(//span[@class='glyphicon glyphicon-pencil'])[1]")).ClickCustom(driver);
            Sleep(3000);
        }
        public ProfilePage EditAnimal()
        {
            editAnimal.ClickCustom(driver);
            return new ProfilePage(driver);
        }
        
        public AdministrationPage SelectAdmin()
        {
            gearIcon.ClickCustom(driver);
            admin.ClickCustom(driver);
            return new AdministrationPage(driver);
        }

        public NewIntakePage ClickAdd()
        {
            addBtn.ClickCustom(driver);
            return new NewIntakePage(driver);
        }
    }
}
