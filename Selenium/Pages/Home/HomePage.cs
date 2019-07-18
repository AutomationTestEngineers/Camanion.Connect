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
        private IWebElement newIntake = null, editAnimal = null, shelterId = null, siteId = null, searchValue = null,
            procedureId = null, veterinarianId = null, technicianId = null;

        [FindsBy(How = How.XPath, Using = "(//button[@id='vetBag'])[1]")]
        private IWebElement vetBag = null;

        [FindsBy(How = How.XPath, Using = "(//button[@id='vetBag'])[1]/../ul/li[text()=' Procedure ']")]
        private IWebElement procedure = null;        

        [FindsBy(How = How.XPath, Using = "//button[@data-cy='addIntake']")]
        private IWebElement addBtn = null;

        [FindsBy(How = How.XPath, Using = "//header[@id='shellHeader']/nav/div/div[2]/div/ul/li/a")]
        private IWebElement email = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/input")]
        private IWebElement searchInput = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='input-group inline-input']/span/button")]
        private IWebElement searchButton = null;

        [FindsBy(How = How.XPath, Using = "//button[@class='SearchBar_button']")]
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

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'OK')]")]
        private IWebElement ok = null;


        [FindsBy(How = How.XPath, Using = "//tbody/tr/td[3]")]
        private IList<IWebElement> searchList = null;

        [FindsBy(How = How.XPath, Using = "(//div[@id='mainmenu']//span[@class='input-group-btn']/label)[1]//i[1]")]
        private IWebElement activeOnly = null;

        [FindsBy(How = How.XPath, Using = "(//div[@class='SearchBar_checkbox'])[1]//span[1]")]
        private IWebElement activeOnly_new = null;

        [FindsBy(How = How.CssSelector, Using = "#mainmenu > form > div > div > div > div > select > option:nth-child(1)")]
        private IWebElement searchDropDown = null;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Save']/..")]
        private IWebElement save = null;

        [FindsBy(How = How.CssSelector, Using = "div[class='QuickAdd-container']")]
        private IWebElement addAnimal = null;

        [FindsBy(How = How.XPath, Using = "//span[text()='Save Procedure']")]
        private IWebElement saveProcedure = null;

        [FindsBy(How = How.LinkText, Using = "Admin Settings")]
        private IWebElement adminSetting = null;

        public void ChangeShelter_New(string shelterName)
        {
            Sleep(300);
            actions.MoveToElement(menu).Click().Build().Perform();
            //menu.ClickCustom(driver);
            Sleep(100);
            selection.Where(t => t.Text.Contains("Change Shelter Location")).FirstOrDefault().ClickCustom(driver);
            shelterId.ClickCustom(driver);
            FindBy(By.XPath($"//div[normalize-space()='{shelterName}']")).ClickCustom(driver);
            if (shelterName == "Demo Shelter" && FindBy(siteId.GetLocator(), 1, true) != null)
            {
                siteId.ClickCustom(driver);
                FindBy(By.XPath("//div[normalize-space()='Demo Site']")).ClickCustom(driver);
            }
            save.ClickCustom(driver);
        }

        public void ChangeShelter(string shelterName)
        {
            if (!string.IsNullOrEmpty(Config.NewVersion))
                ChangeShelter_New(shelterName);
            else
            {
                gearIcon.ClickCustom(driver);
                changeShelter.ClickCustom(driver);
                FindBy(By.XPath($"//strong[normalize-space()='{shelterName}']"), 10).ClickCustom(driver);
                ok.ClickCustom(driver);
                if (shelterName == "Demo Shelter")
                {
                    FindBy(By.XPath($"//strong[normalize-space()='Demo Site']"), 10).ClickCustom(driver);
                    ok.ClickCustom(driver);
                }
            }
        }

        public string GetUserName()
        {
            return email.GetText(driver);
        }

        public void EnterSearch(string text)
        {
            searchInput.SendKeysWrapper(text, driver);
            searchButton.ClickCustom(driver);
        }

        public List<string> getSearchList()
        {
            return searchList.GetText(driver);
        }

        public void SearchAnimal(string searchName)
        {
            Sleep(1500);
            ScreenBusy();
            //if (searchName == null)
            //    searchName = Parameter.Get<string>("AnimalName");

            if (!string.IsNullOrEmpty(Config.NewVersion))
                SearchAnimal_New(searchName);
            else
            {
                try
                {
                    activeOnly.Click();
                    if (activeOnly.GetAttribute("class").Contains("ng-untouched ng-valid ng-dirty ng-valid-parse ng-empty"))
                        activeOnly.ClickCustom(driver);
                }
                catch { }
                searchDropDown.ClickCustom(driver);
                searchInput.SendKeysWrapper(searchName, driver);
                searchButton.ClickCustom(driver);
            }
        }

        public void SearchAnimal_New(string searchName = null)
        {
            ClickActiveOnly();
            searchValue.SendTextAndSelect(searchName, driver, true);
            searchButton_New.ClickCustom(driver);
        }

        public void ClickActiveOnly()
        {
            searchValue.ClickCustom(driver);
            if (activeOnly_new.GetCssValue("color") != "rgba(161, 102, 166, 1)")
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
            Sleep(300);
            if (!string.IsNullOrEmpty(Config.NewVersion))
            { 
                menu.ClickCustom(driver);
                Sleep(500);
                adminSetting.ClickCustom(driver);
            }
            else
            {
                Wait(ExpectedConditions.ElementExists(gearIcon.GetLocator()));
                gearIcon.ClickCustom(driver);
                admin.ClickCustom(driver);
            }
            return new AdministrationPage(driver);
        }

        public NewIntakePage ClickAdd()
        {
            if (!string.IsNullOrEmpty(Config.NewVersion))
                ClickAddAnimal();
            else
                addBtn.ClickCustom(driver);
            return new NewIntakePage(driver);
        }

        public NewIntakePage ClickAddAnimal()
        {
            Sleep(1000);
            addAnimal.ClickCustom(driver);
            return new NewIntakePage(driver);
        }

        public ProfilePage AddProcedure(string name)
        {
            Sleep(1000);
            vetBag.ClickCustom(driver);
            procedure.ClickCustom(driver);
            FindBy(By.XPath($"//div[contains(text(),'{name}')]"), 20);
            procedureId.ClickCustom(driver);
            FindBy(By.XPath($"(//div[@id='{nameof(procedureId)}']//div[2]/div/div[text()])[1]")).ClickCustom(driver, true);
            veterinarianId.ClickCustom(driver);
            FindBy(By.XPath($"(//div[@id='{nameof(veterinarianId)}']//div[2]/div/div[text()])[1]")).ClickCustom(driver, true);
            technicianId.ClickCustom(driver);
            FindBy(By.XPath($"(//div[@id='{nameof(technicianId)}']//div[2]/div/div[text()])[1]")).ClickCustom(driver, true);
            do
            {
                Sleep(100);
            } while (!FindBy(By.XPath("//span[text()='Save Procedure']/..")).GetCssValue("background-color").Contains("rgba(58, 174, 104, 1)"));
            saveProcedure.ClickCustom(driver);           
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[text()='Save Procedure']/..")));
            Sleep(200);
            return new ProfilePage(driver);
        }
    }
}
