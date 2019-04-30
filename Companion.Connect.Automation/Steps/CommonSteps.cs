using FluentAssertions;
using Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using System;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    public class CommonSteps : BaseSteps
    {
        HomePage homepage;
        NewIntakePage newIntakePage;
        public CommonSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }

        [Given(@"I Login")]
        public void ILogin()
        {
            homepage = loginPage.Login();
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

        [When(@"I Click New Intake")]
        public void WhenIClickNewIntake()
        {
            homepage.NewAddIntake();
        }

    }
}
