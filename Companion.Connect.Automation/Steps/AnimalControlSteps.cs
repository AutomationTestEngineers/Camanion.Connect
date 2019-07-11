using Configuration;
using FluentAssertions;
using Selenium.Pages;
using Selenium.Pages.Intake;
using Selenium.Pages.Outcome;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    [Scope(Feature = "AnimalControl")]
    public class AnimalControlSteps : BaseSteps
    { 
        public AnimalControlSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }  

    }
}
