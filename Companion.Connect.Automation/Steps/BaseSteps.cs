using OpenQA.Selenium;
using Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    public class BaseSteps
    {
        public LoginPage loginPage = null;
        public HomePage homePage =null;
        public PersonPage personPage =null;
        public BehaviorPage behaviorPage = null;
        public MedicalPage medicalPage = null;
        public DetailsPage detailsPage =null;
        public ProfilePage profilePage =null;
        public NewIntakePage newIntake =null;
        public AnimalPage animalPage =null;

        protected IWebDriver driver;               
        protected readonly ScenarioContext scenarioContext;

        public BaseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            scenarioContext.TryGetValue<IWebDriver>(out driver);
            scenarioContext.TryGetValue<LoginPage>(out loginPage);
        }

    }
}
