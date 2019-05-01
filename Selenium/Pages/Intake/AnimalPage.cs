using Configuration;
using OpenQA.Selenium;
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
        private IWebElement addAnimal = null, animalName=null;

        public void AddAnimal()
        {
            addAnimal.ClickCustom(driver);
            animalName.SendKeysWrapper("Animal_" + FakeData.FirstName, driver);
        }
    }
}
