using Configuration;
using OpenQA.Selenium;
using Selenium.Pages;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    public class PersonPage : BasePage
    {
        public PersonPage(IWebDriver driver): base(driver) { }

        [FindsBy]
        private IWebElement searchButton=null, addPerson=null, nextButton=null;

        [FindsBy(How = How.CssSelector, Using = "#save-person")]
        private IWebElement nextBtn = null;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'Search')][contains(@ng-model,'vm.')]")]
        private IWebElement search = null;

        [FindsBy(How = How.XPath, Using = "//div[3]//a[contains(text(),'Select')]")]
        private IList<IWebElement> select = null;


        public string GetheaderName()
        {
            return FindBy(By.XPath("//*[@id='content']/div[3]/div[1]//div//h1")).GetText(driver);
        }
        public void SearchPartner(string partner)
        {
            search.SendKeysWrapper(partner, driver);
            searchButton.ClickCustom(driver);
        }

        public AnimalPage SelectPerson(string name=null)
        {            
            switch (name)
            {
                //case "publicstray":
                //    {
                //        FindBy(By.XPath($"//tbody/tr[td[2][contains(text(),'{name}')]]/td//a[contains(text(),'Select')]")).ClickCustom(driver);
                //        nextBtn.ClickCustom(driver);
                //        break;
                //    }
                //case "surrender":
                    
                //    break;
                //case "return":
                    
                //    break;
                //case "transfer":
                    
                //    break;
                case "Animal Control":
                    SearchPartner(Config.Person);
                    select.FirstOrDefault().ClickCustom(driver);
                    nextButton.ClickCustom(driver);
                    break;
                case "service":
                    
                    break;
                default:
                    throw new Exception("Please Provide Intake from :{publicstray,surrender,return,transfer, animalcontrol, service}");

            }
            
            return new AnimalPage(driver);
        }

    }
}
