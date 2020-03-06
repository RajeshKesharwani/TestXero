using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using UnleasedBDD.utils;
using log4net;
using AventStack.ExtentReports;

namespace UnleasedBDD.pages
{
    [Binding]
    class AccountCreation
    {

        private IWebDriver driver;
        private WebDriverWait wait;
        private OtherUtils utilspage;
        private readonly ILog log;
        private readonly ExtentTest test;
        private readonly ScenarioContext context;

        public AccountCreation(IWebDriver driver, ScenarioContext context)
        {
            this.driver = driver;
            this.context = context;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            utilspage = new OtherUtils(driver);
            log = context.Get<ILog>("log");
            test = context.Get<ExtentTest>("extentTest");

        }

        #region DashboardElements

        [FindsBy(How = How.XPath, Using = "//a[text()='Connect your bank accounts']")]
        private IWebElement link_account;

        [FindsBy(How = How.XPath, Using = "//ul[contains(@id,'dataview')])[1]")]
        private IWebElement dropdown_banks;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'accountname')]")]
        private IWebElement txt_accName;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'accounttype')]")]
        private IWebElement txt_acctype;

        [FindsBy(How = How.XPath, Using = "//input[@id='accountnumber-1068-inputEl']")]
        private IWebElement txt_accNumber;

        [FindsBy(How = How.XPath, Using = "//span[text()='Continue']")]
        private IWebElement continuebutton;


        #endregion

        public void AddAccount(string Bank, string Anumber, string Type, string BankAcct)
        {

            test.Log(Status.Pass,"Login Successful");
            link_account.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[contains(@id,'xui-searchfield')]")));
            IWebElement selectBank = driver.FindElement(By.XPath("//li[text()='" + Bank + "']"));
            selectBank.Click();
            wait.Until(x => txt_accName.Displayed);
            txt_accName.SendKeys(BankAcct);
            txt_acctype.Click();
            IWebElement selectAccounttype = driver.FindElement(By.XPath("//*[text()='" + Type + "']"));
            selectAccounttype.Click();
            txt_acctype.SendKeys(Keys.Tab);
            txt_accNumber.SendKeys(Anumber);
            test.Log(Status.Pass, "Entered details");
            Thread.Sleep(2000);
            continuebutton.Click();
            Thread.Sleep(6000);
            
        }


        public void VerifyAccount(string BankAcct)
        {
            
            try
            {
                IWebElement NewAddedAcct = driver.FindElement(By.XPath("//div[contains(text(),'" + BankAcct + "')]"));
                if (NewAddedAcct.Displayed)
                {
                    log.Info("Account found");
                    test.Log(Status.Pass, "Account found");
                }
                    
            } catch(ElementNotVisibleException ex)
            {
                log.Fatal("Account not found");
                test.Log(Status.Fail, "Account not found");
                throw ex;
            }
            
        }
    }
}