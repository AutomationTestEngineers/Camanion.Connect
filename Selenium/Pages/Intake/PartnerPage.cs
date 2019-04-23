using OpenQA.Selenium;
using Selenium.Pages;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    public class PartnerPage : BasePage
    {
        public PartnerPage(IWebDriver driver): base(driver) { }

        
        public string GetheaderName()
        {
            return FindBy(By.XPath("//*[@id='content']/div[3]/div[1]/div/progress-step/div/div[1]/div/h1")).GetText(driver);
        }
    }
}
