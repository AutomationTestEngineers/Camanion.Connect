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
        
        
        public PersonPage Select_Intake(string intakeName)
        {            
            FindBy(By.XPath($"//a[contains(text(),'{intakeName}')]")).ClickCustom(driver); // Select Intake Button  
            return new PersonPage(driver);
        }


    }
}
