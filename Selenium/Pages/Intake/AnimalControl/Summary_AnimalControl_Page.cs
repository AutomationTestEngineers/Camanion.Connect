using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake.AnimalControl
{
    public class Summary_AnimalControl_Page : BasePage
    {
        public Summary_AnimalControl_Page(IWebDriver driver): base(driver) { }

        [FindsBy]
        private IWebElement nextBtn = null;

        public void EnterSummary()
        {

        }
    }
}
