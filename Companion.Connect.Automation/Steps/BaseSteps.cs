using OpenQA.Selenium;
using Selenium.Pages;
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
        protected readonly ScenarioContext scenarioContext;

        public BaseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            scenarioContext.TryGetValue<IWebDriver>(out driver);
            scenarioContext.TryGetValue<LoginPage>(out loginPage);
        }

    }
}
