using Selenium;
using Selenium.Pages.Intake;
using Selenium.Pages.Outcome;
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

        HomePage home => new HomePage(driver);
        NewOutComePage newOutcomePage;



        [When(@"I Enter Details ""(.*)""")]
        public void WhenIEnterDetails(string intakeType)
        {
            (new Selenium.Pages.Intake.DetailsPage(driver)).EnterDetails(intakeType);
        }

        [When(@"I Add Procedure")]
        public void WhenIAddProcedure()
        {
            profilePage = home.AddProcedure(scenarioContext.Get<string>("AnimalName"));
        }

        [When(@"I Request Medical Exam")]
        public void WhenIRequestMedicalExam()
        {
            profilePage.MedicalExam();
        }

        [When(@"I Request Behavior Exam")]
        public void WhenIRequestBehaviorExam()
        {
            profilePage.BehaviorExam();
        }


        [When(@"I Enter Animal Microchip Details")]
        public void WhenIEnterAnimalMicrochipDetails()
        {
            profilePage.EnterMicroChipDetails();
        }

        [When(@"I Enter Animal Rabies Vaccine Details And Realase Holds")]
        public void WhenIEnterAnimalRabiesVaccineDetailsAndRealaseHolds()
        {
            profilePage.EnterAnimalRabiesVaccineDetails();
        }

        [When(@"I Click on ""(.*)"" Outcome")]
        public void WhenIClickOnOutcome(string name)
        {
            newOutcomePage = profilePage.ClickNewOutcome();
            newOutcomePage.SelectOutcome(name);
        }


    }
}
