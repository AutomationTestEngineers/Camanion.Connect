using Selenium.Pages.Intake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{

    [Binding]
    [Scope(Feature = "Service")]
    public sealed class ServiceSteps : BaseSteps
    {
        public ServiceSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }

        [When(@"I Enter Details ""(.*)""")]
        public void WhenIEnterDetails(string intakeType)
        {
            (new DetailsPage(driver)).EnterDetails(intakeType);
        }

    }
}
