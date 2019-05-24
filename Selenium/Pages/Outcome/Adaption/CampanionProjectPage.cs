using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome.Adaption
{
    public class CampanionProjectPage : BasePage
    {
        public CampanionProjectPage(IWebDriver driver) : base(driver) { }

        //[FindsBy]
        //private IWebElement nextButton = null;

        [FindsBy(How = How.XPath, Using = "(//button[@id='nextButton'])[2]")]
        private IWebElement next = null;



        public ContractPage ClickNext()
        {            
            Signature(true);
            ClickWithLoop(next.GetLocator());
            return new ContractPage(driver);
        }
    }
}
