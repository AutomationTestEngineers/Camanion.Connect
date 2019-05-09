using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Diagnostics;
using Selenium;
using Companion.Connect.Automation;
using Selenium.Pages;
using Configuration;

namespace Epiq.ECA.E2ETest.Global
{
    [Binding]
    public sealed class Hooks
    {
        /// <summary>
        /// For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        /// </summary>

        private readonly ScenarioContext scenarioContext;
        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetUp(FeatureContext featureContext)
        {
            Parameter.Collect("Parameter.xml",new List<string> { "Parameter", $"Parameter/{this.scenarioContext.ScenarioInfo.Title}" });
            IWebDriver driver;
            if (featureContext.Keys.Count > 0)
            {
                driver = featureContext.Get<IWebDriver>();
            }
            else
            {
                driver = (new WebDriver()).InitDriver();
            }
            scenarioContext.Set<IWebDriver>(driver);
            var login = new LoginPage(driver);
            scenarioContext.Set<LoginPage>(login);
        }

        [AfterScenario]
        public void CleanUp(FeatureContext featureContext)
        {
            IWebDriver driver = scenarioContext.Get<IWebDriver>();
            if (scenarioContext.TestError != null)
                driver.GetScreenShot(scenarioContext.ScenarioInfo.Title);
            driver.Quit();
            Parameter.ClearParameters();
        }

        [AfterStep]
        public void LogStepResult()
        {
            //This method is here to fix the bug in SpecFlow
            //the bug is when using parallel execution the test output log is not written to the tests
            //see https://github.com/techtalk/SpecFlow/issues/737

            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(LevelOfParallelismAttribute), false);
            LevelOfParallelismAttribute attribute = null;

            if (attributes.Length > 0)
            {
                attribute = attributes[0] as LevelOfParallelismAttribute;
                var levelOfParallelism = (int)attribute.Properties.Get(attribute.Properties.Keys.First());

                if (levelOfParallelism > 1)
                {
                    string stepText = scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + scenarioContext.StepContext.StepInfo.Text;
                    Console.WriteLine(stepText);
                    var stepTable = scenarioContext.StepContext.StepInfo.Table;
                    if (stepTable != null && stepTable.ToString() != "") Console.WriteLine(stepTable);
                    var error = scenarioContext.TestError;
                    Console.WriteLine(error != null ? "-> error: " + error.Message : "-> done.");
                }
            }
        }
    }
}