﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages.Intake.AnimalControl
{
    public class Detials_AnimalControl_Page : BasePage
    {
        public Detials_AnimalControl_Page(IWebDriver driver): base(driver) { }

        [FindsBy]
        private IWebElement site = null, subSite=null, location=null, subLocation=null, intakeDetailsnote=null;

        [FindsBy(How = How.Name, Using = "IntakeSubType")]
        private IWebElement intakeSubType = null;

        [FindsBy(How = How.Name, Using = "AnimalCondition")]
        private IWebElement animalCondition = null;

        [FindsBy(How = How.Id, Using = "intakeDetails-save")]
        private IWebElement saveNotes = null;

        [FindsBy(How = How.XPath, Using = "//button[@data-ng-click='vm.done()']")]
        private IWebElement saveAndClose = null;

        public HomePage EnterDetailsInfo()
        {
            intakeSubType.SelectByIndex(driver);
            animalCondition.SelectByIndex(driver);
            site.SelectByIndex(driver);
            subSite.SelectByIndex(driver);
            location.SelectByIndex(driver);
            subLocation.SelectByIndex(driver);
            intakeDetailsnote.SendKeysWrapper("intake Detailsnote", driver);
            saveNotes.ClickCustom(driver);
            saveAndClose.ClickCustom(driver);
            Sleep(3000);
            return new HomePage(driver);
        }
    }
}
