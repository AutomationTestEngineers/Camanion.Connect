using Configuration;
using FluentAssertions;
using Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using Selenium.Pages.Intake.AnimalControl;
using Selenium.Pages.Outcome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    [Scope(Feature= "AnimalControl")]
    public class AnimalControlSteps : BaseSteps
    {
        PartnerPageAnimalControl partnerPage;
        AnimalPageAnimalControl animalPage;
        Behavior_AnimalControl_Page behaviorPage;
        Medical_AnimalControl_Page medicalPage;
        Detials_AnimalControl_Page detailsPage;
        HomePage homePage;
        ProfilePage profilePage;
        NewOutComePage newOutcomePage;
        OutcomeSearchPage outcomeSearchPage;


        public AnimalControlSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }

        [When(@"I Select ""(.*)"" Intake")]
        public void WhenISelectIntake(string intake)
        {
            (new NewIntakePage(driver)).Select_Intake(intake);
            partnerPage = new PartnerPageAnimalControl(driver);
        }

        [When(@"I Select Partner ""(.*)""")]
        public void WhenISelectPartner(string partner)
        {
            animalPage = partnerPage.SearchPartner(partner);
        }

        [When(@"I Add Animal")]
        public void WhenIAddAnimal()
        {
            behaviorPage = animalPage.AddAnimal();
        }

        [When(@"I Enter Behavior")]
        public void WhenIEnterBehavior()
        {
            medicalPage = behaviorPage.EnterBehavior();
        }

        [When(@"I Enter Medical")]
        public void WhenIEnterMedical()
        {
            detailsPage = medicalPage.EnterMedicalInfo();
        }

        [When(@"I Enter Details")]
        public void WhenIEnterDetails()
        {
            homePage = detailsPage.EnterDetailsInfo();
        }

        [Then(@"User Should See Animal Name")]
        public void ThenUserShouldSeeAnimalName()
        {
            homePage.SearchAnimal();
            homePage.GetAnimalName().Should().Contain(Parameter.Get<string>("AnimalName"));
            profilePage = homePage.EditAnimal();
        }

        [When(@"I Enter Animal Details To Profile")]
        public void WhenIEnterAnimalDetailsToProfile()
        {
            profilePage.EnterMicroChipDetails();
            profilePage.EnterAnimalDetails();
        }

        [When(@"I Realease Animal Holds")]
        public void WhenIRealeaseAnimalHolds()
        {
            homePage.ClickPencilIcon();
            profilePage.ReleaseHolds();
        }

        [When(@"I Click New Outcome Button")]
        public void WhenIClickNewOutcomeButton()
        {
            newOutcomePage = profilePage.ClickNewOutcome();
        }

        [When(@"I Select ""(.*)""")]
        public void WhenISelect(string btnName)
        {
            newOutcomePage.SelectOutcome(btnName);
        }

        [When(@"I Delete Recent Outcome")]
        public void WhenIDeleteRecentOutcome()
        {
            outcomeSearchPage = (new AdministrationPage(driver)).DeleteOutComes();
            outcomeSearchPage.SearchOutcome();
        }

        [When(@"I Delete Recent Intake")]
        public void WhenIDeleteRecentIntake()
        {
            
        }


    }
}
