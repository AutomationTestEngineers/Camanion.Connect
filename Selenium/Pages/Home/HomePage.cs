using Configuration;
using FluentAssertions;
using OpenQA.Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selenium
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement newIntake = null, editAnimal=null, adminDate=null, scheduleDate=null, veterinarian=null, technician=null,
            careComments=null, shelterId=null, siteId=null, searchValue=null;  


        [FindsBy(How = How.XPath, Using = "(//button[@id='vetBag'])[1]")]
        private IWebElement vetBag = null;

        [FindsBy(How = How.XPath, Using = "(//button[@id='vetBag'])[1]/../ul/li[5]")]
        private IWebElement procedure = null;

        [FindsBy(How = How.Name, Using = "careActivity")]
        private IWebElement careActivity = null;

        [FindsBy(How = How.XPath, Using = "//button[@ng-click='vm.save()']")]
        private IWebElement saveAndClose = null;

        [FindsBy(How = How.XPath, Using = "//*[@id='procedure']")]
        private IWebElement procedures = null;        

        //[FindsBy(How = How.Name, Using = "careActivity")]
        //private IWebElement careActivity = null;

        //[FindsBy(How = How.XPath, Using = "//a[@class='QuickAdd-button']")]
        //private IWebElement addBtn = null;

        [FindsBy(How = How.XPath, Using = "//button[@data-cy='addIntake']")]
        private IWebElement addBtn = null;

        [FindsBy(How = How.XPath, Using = "//header[@id='shellHeader']/nav/div/div[2]/div/ul/li/a")]
        private IWebElement email = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/input")]
        private IWebElement searchInput = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/span/button")]
        private IWebElement searchButton = null;

        [FindsBy(How = How.XPath, Using = "//button[@class='SearchBar-button']")]
        private IWebElement searchButton_New = null;

        [FindsBy(How = How.XPath, Using = "//div[@id='mainmenu']//button/i[@class='icon icon-setting']")]
        private IWebElement gearIcon = null;

        [FindsBy(How = How.XPath, Using = "(//*[@role='presentation'])[last()]")]
        private IWebElement menu = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='Account-container']/div")]
        private IList<IWebElement> selection = null;

        [FindsBy(How = How.XPath, Using = "//li[2]//a[contains(text(),'Change Shelter Selection')]")]
        private IWebElement changeShelter = null;

        [FindsBy(How = How.XPath, Using = "//li[2]//a[contains(text(),'Admin')]")]
        private IWebElement admin = null;

        //[FindsBy(How = How.LinkText, Using = "/admin")]
        //private IWebElement admin = null;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'OK')]")]
        private IWebElement ok = null;
        

        [FindsBy(How = How.XPath, Using = "//tbody/tr/td[3]")]
        private IList<IWebElement> searchList = null;

        [FindsBy(How = How.XPath, Using = "(//div[@id='mainmenu']//span[@class='input-group-btn']/label)[1]//i[1]")]
        private IWebElement activeOnly = null;

        [FindsBy(How = How.XPath, Using = "(//div[@class='SearchBar-checkbox'])[1]//span[1]")]
        private IWebElement activeOnly_new = null;

        [FindsBy(How = How.CssSelector, Using = "#mainmenu > form > div > div > div > div > select > option:nth-child(1)")]
        private IWebElement searchDropDown = null;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Save']/..")]
        private IWebElement save = null;

        [FindsBy(How = How.CssSelector, Using = "div[class='QuickAdd-container']")]
        private IWebElement addAnimal = null;

        public void ChangeShelter_New(string shelterName)
        {
            menu.ClickCustom(driver);
            Sleep(100);
            selection.Where(t => t.Text.Contains("Change Shelter Location")).FirstOrDefault().ClickCustom(driver);
            shelterId.ClickCustom(driver);
            FindBy(By.XPath($"//div[normalize-space()='{shelterName}']")).ClickCustom(driver);
            if (shelterName == "Demo Shelter")
            {
                siteId.ClickCustom(driver);
                FindBy(By.XPath("//div[normalize-space()='Demo Site']")).ClickCustom(driver);
            }
            save.ClickCustom(driver);
        }

        public void ChangeShelter(string shelterName)
        {

            gearIcon.ClickCustom(driver);
            changeShelter.ClickCustom(driver);
            FindBy(By.XPath($"//strong[normalize-space()='{shelterName}']"), 10).ClickCustom(driver);
            ok.ClickCustom(driver);
            if(shelterName== "Demo Shelter")
            {
                FindBy(By.XPath($"//strong[normalize-space()='Demo Site']"), 10).ClickCustom(driver);
                ok.ClickCustom(driver);
            }
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
            ScreenBusy();
            if (searchName == null)
                searchName = Parameter.Get<string>("AnimalName");

            //ClickActiveOnly();
            //FindBy(By.XPath("//div[@id='searchType']/div")).ClickCustom(driver);
            //FindBy(By.XPath("(//div[text()='Animal Name'])[last()]")).ClickCustom(driver);
            try
            {
                activeOnly.Click();
                if (activeOnly.GetAttribute("class").Contains("ng-untouched ng-valid ng-dirty ng-valid-parse ng-empty"))
                    activeOnly.ClickCustom(driver);
            }
            catch { }
            

            searchDropDown.ClickCustom(driver);
            searchInput.SendKeysWrapper(searchName, driver);
            //searchValue.SendKeysWrapper(searchName, driver);
            searchButton.ClickCustom(driver);
        }

        public void SearchAnimal_New(string searchName = null)
        {
            Sleep(1000);
            ScreenBusy();
            if (searchName == null)
                searchName = Parameter.Get<string>("AnimalName");
            ClickActiveOnly();
            searchValue.SendKeysWrapper(searchName, driver);
            searchButton_New.ClickCustom(driver);
            //if(FindBy(By.XPath("//div[@id='searchType']//div[text()='Animal Name']"),1,true)!=null)
            //{
            //    FindBy(By.XPath("//div[@id='searchType']/div")).ClickCustom(driver);
            //    FindBy(By.XPath("(//div[text()='Animal Name'])[last()]")).ClickCustom(driver);
            //}

        }

        public void ClickActiveOnly()
        {
            searchValue.ClickCustom(driver);
            if (activeOnly_new.GetCssValue("color")!= "rgba(161, 102, 166, 1)")
                activeOnly_new.ClickCustom(driver);
        }

        public string GetAnimalName()
        {
            Sleep(500);
            return FindBy(By.XPath("//table[@class='table table-custom']//td[3]")).Text;            
        }

        public void ClickPencilIcon()
        {
            FindBy(By.XPath("(//span[@class='glyphicon glyphicon-pencil'])[1]")).ClickCustom(driver);
            Sleep(1000);
        }

        public ProfilePage EditAnimal()
        {
            editAnimal.ClickCustom(driver);
            return new ProfilePage(driver);
        }
        
        public AdministrationPage SelectAdmin()
        {
            //Wait(ExpectedConditions.ElementExists(gearIcon.GetLocator()));
            //gearIcon.ClickCustom(driver);
            //admin.ClickCustom(driver);
            menu.ClickCustom(driver);
            Sleep(100);
            FindBy(By.XPath("//a[text()='Admin Settings']")).ClickCustom(driver);
            return new AdministrationPage(driver);
        }

        public NewIntakePage ClickAdd()
        {
            addBtn.ClickCustom(driver);
            return new NewIntakePage(driver);
        }

        public NewIntakePage ClickAddAnimal()
        {
            Sleep(1000);
            addAnimal.ClickCustom(driver);
            return new NewIntakePage(driver);
        }

        public ProfilePage AddProcedure()
        {
            Sleep(1000);
            vetBag.ClickCustom(driver);
            procedure.ClickCustom(driver);
            driver.Popup(true);
            Sleep(1000);
            careActivity.SelectByIndex(driver, 1);
            adminDate.SendKeysWrapper(DateTime.Today.ToShortDateString(), driver);
            adminDate.SendKeys(Keys.Tab);
            veterinarian.SelectByIndex(driver, 1);
            technician.SelectByIndex(driver, 1);
            careComments.SendKeysWrapper("Care Comments", driver);
            saveAndClose.ClickCustom(driver);
            Sleep(200);
            ScreenBusy();
            procedures.Displayed.Should().BeTrue("Procedures Not Displayed");

            return new ProfilePage(driver);
        }
    }
}
