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

namespace UnleasedBDD.pages
{
    class LogInPage
    {

        private IWebDriver driver;
        private WebDriverWait wait;
    
        public LogInPage(IWebDriver driver)
        {
             this.driver = driver;
             PageFactory.InitElements(driver, this);
             wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

        }

        #region LoginElements

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement txt_userName;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement txt_password;

        [FindsBy(How = How.Id, Using = "submitButton")]
        private IWebElement btn_signIn;

        [FindsBy(How = How.XPath, Using = "//a[text()='Dashboard']")]
        private IWebElement lbl_dashboard;
        

        #endregion

        public void SignIn(string Name)
        {
            txt_userName.SendKeys(ConfigurationManager.AppSettings["userID"]);
            txt_password.SendKeys(ConfigurationManager.AppSettings["password"]);
            btn_signIn.Click();
            wait.Until(ExpectedConditions.TitleContains(Name));

        }

    }
}
