using FluentAssertions;
using Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using Selenium.Pages.Intake.AnimalControl;
using System;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    public sealed class CommonSteps : BaseSteps
    {   
        public CommonSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }

        [Given(@"I Login")]
        public void ILogin()
        {
            homePage = loginPage.Login();
        }

        [When(@"I Change Shelter ""(.*)""")]
        public void WhenIChangeShelter(string shelterName)
        {
            homePage.ChangeShelter(shelterName);
        }

        [When(@"I Search ""(.*)""")]
        public void WhenISearch(string searchText)
        {
            homePage.EnterSearch(searchText);
        }

        [Then(@"User Should See Search Reasult ""(.*)""")]
        public void ThenUserShouldSeeSearchReasult(string searchName)
        {
            homePage.getSearchList().Count.Should().NotBe(0);
            foreach (string s in homePage.getSearchList())
                s.ToLower().Should().Contain(searchName.ToLower());
        }

        [When(@"I Click New Intake")]
        public void WhenIClickNewIntake()
        {
            homePage.NewAddIntake();
        }

        [When(@"I Select ""(.*)"" Intake")]
        public void WhenISelectIntake(string intake)
        {
            (new NewIntakePage(driver)).Select_Intake(intake);
            partnerPage = new PartnerPageAnimalControl(driver);
        }

        [When(@"I Select Partner ""(.*)""")]
        public void WhenISelectPartner(string partner)
        {
            partnerPage.SearchPartner(partner);
        }
    }
}
