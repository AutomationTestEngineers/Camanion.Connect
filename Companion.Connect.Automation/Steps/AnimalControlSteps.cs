using Configuration;
using FluentAssertions;
using Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using Selenium.Pages.Intake.AnimalControl;
using Selenium.Pages.Outcome;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    [Scope(Feature = "AnimalControl")]
    public class AnimalControlSteps : BaseSteps
    {
        Behavior_AnimalControl_Page behaviorPage;
        Medical_AnimalControl_Page medicalPage;
        Detials_AnimalControl_Page detailsPage;
        ProfilePage profilePage;
        NewOutComePage newOutcomePage;
        OutcomeSearchPage outcomeSearchPage;


        public AnimalControlSteps(ScenarioContext scenarioContext) : base(scenarioContext) { }        

        
        [When(@"I Add Animal")]
        public void WhenIAddAnimal()
        {
            behaviorPage = (new AnimalPageAnimalControl(driver)).AddAnimal();
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
