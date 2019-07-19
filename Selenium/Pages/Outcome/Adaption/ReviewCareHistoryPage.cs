using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Outcome.Adaption
{
    public class ReviewCareHistoryPage :BasePage
    {
        public ReviewCareHistoryPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How=How.XPath,Using = "//div[@id='animal-condition']/div[2]/div/div")]
        private IList<IWebElement> animalCondition = null;

        [FindsBy(How = How.XPath, Using = "//div[@id='shelter-feedback']/div[2]/div/div")]
        private IList<IWebElement> shelterFeedback = null;

        [FindsBy(How =How.Id,Using = "next-step")]
        private IWebElement next = null;


        public CampanionProjectPage EnterReviewHistory()
        {
            //FindBy(By.Id("shelter-feedback"), 1, true).Click();
            //Sleep(20);
            //shelterFeedback[1].Click();
            //Sleep(20);
            //FindBy(By.Id("animal-condition"), 1, true).Click();
            //Sleep(20);
            //animalCondition[1].Click();
            ClickWithLoop(next.GetLocator());
            return new CampanionProjectPage(driver);
        }
    }
}
