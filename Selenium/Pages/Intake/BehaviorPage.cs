using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake
{
    public class BehaviorPage : BasePage
    {
        public BehaviorPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement temperament = null, biteHistory = null, intakeBehaviornote = null;

        [FindsBy(How = How.XPath, Using = "//li[@data-ng-repeat='item in vm.colors'][3]//i[1]")]
        private IWebElement handlingColor = null;

        [FindsBy(How = How.XPath, Using = "//button[@data-ng-click='vm.gotoNextStep()']")]
        private IWebElement nextBtn = null;

        [FindsBy(How = How.Id, Using = "intakeBehavior-save")]
        private IWebElement saveNotes = null;


        public MedicalPage EnterBehavior()
        {
            temperament.SelectByIndex(driver,2);
            biteHistory.SelectByIndex(driver, 2);
            //FindBy(By.XPath($"(//div[@class='row'])[10]//label[contains(.,'{Parameter.Get<string>("Impression")}')]//i[1]")).ClickCustom(driver);
            FindBy(By.XPath("//*[@id='content']/div/section/section/div/form/div[2]/div/div[2]/div[4]/label/span/label/i[1]")).ClickCustom(driver);

            handlingColor.ClickCustom(driver);
            FindBy(By.XPath($"//ul[@ng-show='showTicks']/li[3]")).ClickCustom(driver);
            intakeBehaviornote.SendKeysWrapper("AutomationTest", driver);
            Sleep(100);
            saveNotes.ClickCustom(driver);
            Sleep(300);
            nextBtn.ScrollElement(driver);
            nextBtn.ClickCustom(driver);
            Sleep(400);
            return new MedicalPage(driver);
        }
    }
}
