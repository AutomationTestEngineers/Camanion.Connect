using FluentAssertions;
using Selenium;
using Selenium.Pages;
using System;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    public class SmokeSteps : BaseSteps
    {
        HomePage homepage;
        PartnerPage partnerPage;
        public SmokeSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }

        [Given(@"I Enter ""(.*)""")]
        public void GivenIEnter(string args)
        {
            homepage = loginPage.Login(args.Split(',')[0], args.Split(',')[1]);
        }
        [Then(@"User Should see ""(.*)""")]
        public void ThenUserShouldSee(string name)
        {
            homepage.GetUserName().Should().Contain(name);
        }
        [When(@"I Change Shelter ""(.*)""")]
        public void WhenIChangeShelter(string shelterName)
        {
            homepage.ChangeShelter(shelterName);
        }

        [When(@"I Search ""(.*)""")]
        public void WhenISearch(string searchText)
        {
            homepage.EnterSearch(searchText);
        }      

        [Then(@"User Should See Search Reasult ""(.*)""")]
        public void ThenUserShouldSeeSearchReasult(string searchName)
        {            
            homepage.getSearchList().Count.Should().NotBe(0);
            foreach (string s in homepage.getSearchList())
                s.ToLower().Should().Contain(searchName.ToLower());
        }

        [When(@"I Click Intake ""(.*)""")]
        public void WhenIClickIntake(string intake)
        {
            partnerPage = homepage.NewAddIntake(intake);
        }

        [Then(@"User Should See Intake Header ""(.*)""")]
        public void ThenUserShouldSeeIntakeHeader(string intakeMessage)
        {
            partnerPage.GetheaderName().Should().Contain(intakeMessage);
        }

    }
}
