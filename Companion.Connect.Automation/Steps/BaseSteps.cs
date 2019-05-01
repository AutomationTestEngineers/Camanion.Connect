using OpenQA.Selenium;
using Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake.AnimalControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    public class BaseSteps
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected HomePage homePage;
        protected PartnerPageAnimalControl partnerPage;        
        protected readonly ScenarioContext scenarioContext;

        public BaseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            scenarioContext.TryGetValue<IWebDriver>(out driver);
            scenarioContext.TryGetValue<LoginPage>(out loginPage);
        }

    }
}
