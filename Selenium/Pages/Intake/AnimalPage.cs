using Configuration;
using OpenQA.Selenium;
using Selenium.Pages.Intake;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages
{
    public class AnimalPage : BasePage
    {
        public AnimalPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement addAnimal = null, animalName = null, petType = null, years = null, primaryBreed = null, primaryColor = null,
            gender = null, noteType = null, estAdultSize = null, nextButton = null;


        public BehaviorPage AddAnimal()
        {
            Sleep(500);
            ScreenBusy();
            addAnimal.ClickCustom(driver);
            Sleep(3000);
            petType.ClickCustom(driver);
            Sleep();
            petType.SelectDropDown(driver, Parameter.Get<string>("PetType"));
            Sleep();
            Parameter.Add<string>("AnimalName", "Animal_" + FakeData.FirstName);
            animalName.SendKeysWrapper(Parameter.Get<string>("AnimalName"), driver);
            years.SendKeys(Parameter.Get<string>("Years"));
            years.SendKeys(Keys.Tab);
            primaryBreed.SelectByIndex(driver);
            primaryColor.SelectByIndex(driver);
            gender.SelectDropDown(driver, Parameter.Get<string>("Gender"));
            estAdultSize.SelectByIndex(driver);
            if(FindBy(noteType.GetLocator(), 1, true)!=null)
                noteType.SelectByIndex(driver);
            nextButton.ClickCustom(driver);
            Sleep(3000);
            return new BehaviorPage(driver);
        }
    }
}
