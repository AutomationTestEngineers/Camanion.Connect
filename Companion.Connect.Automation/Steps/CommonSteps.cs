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

        [When(@"I Click Add")]
        public void WhenIClickAdd()
        {
            newIntake = homePage.ClickAdd();
        }

        [When(@"I Search Animal")]
        public void WhenISearchAnimal()
        {
            homePage.SearchAnimal();
            driver.Popup();
            homePage.ClickPencilIcon();
        }

        [Then(@"User Should See Search Reasult ""(.*)""")]
        public void ThenUserShouldSeeSearchReasult(string searchName)
        {
            homePage.getSearchList().Count.Should().NotBe(0);
            foreach (string s in homePage.getSearchList())
                s.ToLower().Should().Contain(searchName.ToLower());
        }        

        [When(@"I Select ""(.*)"" Intake")]
        public void WhenISelectIntake(string intake)
        {
            personPage = newIntake.Select_Intake(intake);
        }

        [When(@"I Select Partner ""(.*)""")]
        public void WhenISelectPartner(string partner)
        {
            animalPage = (!string.IsNullOrWhiteSpace(partner))?personPage.SearchPartner(partner): personPage.WithNoPerson();
        }

        [When(@"I Enter Payment Details")]
        public void WhenIEnterPaymentDetails()
        {
            animalPage = (new Selenium.Pages.Intake.PaymentPage(driver)).EnterPayment(0);
        }

        [When(@"I Enter Payment Details PublicStray")]
        public void WhenIEnterPaymentDetailsPublicStray()
        {
            animalPage = (new Selenium.Pages.Intake.PaymentPage(driver)).EnterPayment();
        }

        [When(@"I Add Animal")]
        public void WhenIAddAnimal()
        {
            scenarioContext.Add("AnimalName", "Animal_" + FakeData.FirstName);
            behaviorPage = animalPage.AddAnimal(scenarioContext.Get<string>("AnimalName"));
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
            homePage.SearchAnimal(scenarioContext.Get<string>("AnimalName"));
            homePage.GetAnimalName().Should().Contain(scenarioContext.Get<string>("AnimalName"));
            profilePage = homePage.EditAnimal();
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
            homePage.SearchAnimal(scenarioContext.Get<string>("AnimalName"));
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
            outcomeSearchPage.SearchOutcome(scenarioContext.Get<string>("AnimalName"));
            outcomeSearchPage.DeleteOutcome();
        }

        [When(@"I Delete Recent Intake")]
        public void WhenIDeleteRecentIntake()
        {
            administrationPage = homePage.SelectAdmin();
            intakeSearchPage = administrationPage.ClickIntakes();
            intakeSearchPage.SearchIntake(scenarioContext.Get<string>("AnimalName"));
            intakeSearchPage.DeleteIntake();
        }

        [When(@"I Delete Recent Outcome ""(.*)""")]
        public void WhenIDeleteRecentOutcome(string p0)
        {
            administrationPage = homePage.SelectAdmin();
            outcomeSearchPage = administrationPage.ClickOutcomes();
            outcomeSearchPage.SearchOutcome_Specific(p0);
        }

        [When(@"I Delete Recent Intake ""(.*)""")]
        public void WhenIDeleteRecentIntake(string p0)
        {
            administrationPage = homePage.SelectAdmin();
            intakeSearchPage = administrationPage.ClickIntakes();
            intakeSearchPage.SearchIntake_Specific(p0);
        }


        [Then(@"""(.*)"" Message Should Be Display")]
        public void ThenMessageShouldBeDisplay(string message)
        {
            intakeSearchPage.GetToastMessage().Should().Equals(message);            
        }

    }
}
