using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake
{
    public class NewIntakePage : BasePage
    {
        public NewIntakePage(IWebDriver driver) : base(driver) { }  
        
        
        public void Select_Intake(string intakeName)
        {            
            FindBy(By.XPath($"//a[contains(text(),'{intakeName}')]")).ClickCustom(driver); // Select Intake Button            
        }

        public void Populate_Person_Fields(string intakeName)
        {
            var person = new PersonPage(driver);
            person.SelectPerson(intakeName);

        }

        public void Populate_Payment_Fields(string intakeName)
        {
            var payment = new PaymentPage(driver);
        }

        public void Populate_Animal_Fields(string intakeName)
        {
            var animal = new AnimalPage(driver);
            animal.AddAnimal();
        }

        public void Populate_Behavior_Fields(string intakeName)
        {
            var behavior = new BehaviorPage(driver);
        }

        public void Populate_Medical_Fields(string intakeName)
        {
            var medical = new MedicalPage(driver);
        }

        public void Populate_Details_Fields(string intakeName)
        {

        }

        public void Populate_Summary_Fields(string intakeName)
        {
            var summary = new SummaryPage(driver);
        }
    }
}
