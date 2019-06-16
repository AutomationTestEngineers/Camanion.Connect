using Configuration;
using FluentAssertions;
using Selenium;
using Selenium.Pages;
using Selenium.Pages.Intake;
using Selenium.Pages.Outcome;
using TechTalk.SpecFlow;

namespace Companion.Connect.Automation.Steps
{
    [Binding]
    public sealed class CommonSteps : BaseSteps
    {
        NewOutComePage newOutcomePage;
        OutcomeSearchPage outcomeSearchPage;
        AdministrationPage administrationPage;
        IntakeSearchPage intakeSearchPage;
       

        public CommonSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            profilePage = new ProfilePage(driver); ;
        }

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

        //[When(@"I Search ""(.*)""")]
        //public void WhenISearch(string searchText)
        //{
        //    homePage.EnterSearch(searchText);
        //}

        [When(@"I Click Add")]
        public void WhenIClickAdd()
        {
            newIntake = homePage.ClickAdd();            
        }

        [When(@"I Search Animal")]
        public void WhenISearchAnimal()
        {
            //homePage.SearchAnimal();
            //homePage.ClickPencilIcon();
            homePage.GotoProfilePage(Parameter.Get<string>("AnimalName"));
        }

        [Then(@"User Should See Search Reasult ""(.*)""")]
        public void ThenUserShouldSeeSearchReasult(string searchName)
        {
            homePage.getSearchList().Count.Should().NotBe(0);
            foreach (string s in homePage.getSearchList())
                s.ToLower().Should().Contain(searchName.ToLower());
        }

        //[When(@"I Click New Intake")]
        //public void WhenIClickNewIntake()
        //{
        //    newIntake = homePage.NewAddIntake();
        //}

        [When(@"I Select ""(.*)"" Intake")]
        public void WhenISelectIntake(string intake)
        {
            personPage = newIntake.Select_Intake(intake);
        }

        [When(@"I Select Partner ""(.*)""")]
        public void WhenISelectPartner(string partner)
        {
            animalPage = personPage.SearchPartner(partner);
        }

        [When(@"I Enter Payment Details")]
        public void WhenIEnterPaymentDetails()
        {
            animalPage = (new Selenium.Pages.Intake.PaymentPage(driver)).EnterPayment();
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

        [When(@"I Enter Medical ""(.*)""")]
        public void WhenIEnterMedical(string index)
        {
            detailsPage = medicalPage.EnterMedicalInfo(index);
        }
        

        [When(@"I Enter Details")]
        public void WhenIEnterDetails()
        {
            homePage = detailsPage.EnterDetailsInfo();
        }
        [Then(@"User Should See Animal Name")]
        public void ThenUserShouldSeeAnimalName()
        {
            //homePage.SearchAnimal();            
            //homePage.GetAnimalName().Should().Contain(Parameter.Get<string>("AnimalName"));
            //profilePage = homePage.EditAnimal();
            profilePage = homePage.GotoProfilePage(Parameter.Get<string>("AnimalName"));
        }

        [When(@"I Enter Animal Details To Profile")]
        public void WhenIEnterAnimalDetailsToProfile()
        {
            profilePage.EnterMicroChipDetails();
            profilePage.EnterAnimalDetails();
        }

        [When(@"I Click Pencil Icon From Result")]
        public void WhenIClickPencilIconFromResult()
        {
            homePage.ClickPencilIcon();
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
            homePage.SearchAnimal();
            homePage.ClickPencilIcon();
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
            administrationPage = homePage.SelectAdmin();
            outcomeSearchPage = administrationPage.ClickOutcomes();
            outcomeSearchPage.SearchOutcome();
            outcomeSearchPage.DeleteOutcome();
        }

        [When(@"I Delete Recent Intake")]
        public void WhenIDeleteRecentIntake()
        {
            administrationPage = homePage.SelectAdmin();
            intakeSearchPage = administrationPage.ClickIntakes();
            intakeSearchPage.SearchIntake();
            intakeSearchPage.DeleteIntake();            
        }

        [Then(@"""(.*)"" Message Should Be Display")]
        public void ThenMessageShouldBeDisplay(string message)
        {
            //intakeSearchPage.GetToastMessage().Should().Equals(message);            
        }

    }
}
