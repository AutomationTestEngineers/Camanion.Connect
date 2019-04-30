using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake.AnimalControl
{
    public class AnimalPageAnimalControl : BasePage
    {
        public AnimalPageAnimalControl(IWebDriver driver): base(driver) { }

        [FindsBy]
        private IWebElement addAnimal = null, animalName = null, petType=null, years=null, primaryBreed=null, primaryColor=null,
            gender=null, noteType=null, estAdultSize=null, nextButton=null;


        public Behavior_AnimalControl_Page AddAnimal()
        {
            addAnimal.ClickCustom(driver);
            Sleep(3000);
            petType.ClickCustom(driver);
            Sleep();
            petType.SelectDropDown(driver, Parameter.Get<string>("PetType"));
            Sleep();
            Parameter.Add<string>("AnimalName","Animal_" + FakeData.FirstName);
            animalName.SendText(Parameter.Get<string>("AnimalName"), driver);
            years.SendKeys(Parameter.Get<string>("Years"));
            years.SendKeys(Keys.Tab);
            primaryBreed.SelectByIndex(driver);
            primaryColor.SelectByIndex(driver);
            gender.SelectDropDown(driver,Parameter.Get<string>("Gender"));
            estAdultSize.SelectByIndex(driver);
            noteType.SelectByIndex(driver);
            nextButton.ClickCustom(driver);
            return new Behavior_AnimalControl_Page(driver);
        }
    }
}
