using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome
{
    public class NewOutComePage : BasePage
    {
        public NewOutComePage(IWebDriver driver) : base(driver) { }

        //[FindsBy]
        //private IWebElement elem = null;

        public void SelectOutcome(string outCome)
        {            
            FindBy(By.XPath($"//a[@id='outcomeType.Name'][contains(text(),'{outCome}')]")).ClickCustom(driver);
            var death = new DeathOutcomePage(driver);
            switch (outCome)
            {                
                case "Death":                    
                    death.EnterDeathDetails();
                    break;
                case "Euthanasia":                    
                    death.EnterDeathEuthanasia();
                    break;
                case "Transfer":
                    death.EnterDeathTransfer();
                    break;
                case "Return to Owner":

                    break;
                case "Adoption":
                    break;
            }
        }
    }
}
