using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AventStack.ExtentReports;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UnleasedBDD.pages;

namespace UnleasedBDD.StepDefinitions
{
    [Binding]
    public sealed class AddAccountSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private IWebDriver driver;
        private readonly ScenarioContext context;
        private readonly ILog log;
        private readonly ExtentTest test;
        private LogInPage login;
        private AccountCreation acctcreate;
        public AddAccountSteps(ScenarioContext context)
        {
            this.context = context;
            driver = context.Get<IWebDriver>("driver");
            log = context.Get<ILog>("log");
            test = context.Get<ExtentTest>("extentTest");
            login = new LogInPage(driver);
            acctcreate = new AccountCreation(driver,context);
        }

        [Given(@"I have logged in to xero prod(.*)")]
        public void GivenIHaveLoggedInToXeroProd(string AccName)
        {
            login.SignIn(AccName);
        }
        [Given(@"I input my bank account number(.*),(.*),(.*),(.*)")]
        public void GivenIInputMyBankAccountNumber(string Bank, string BankAccountName, string Number, string Type)
        {
            acctcreate.AddAccount(Bank, Number, Type, BankAccountName);
        }



        [Then(@"the (.*) should get added successfully")]
        public void ThenTheAccountShouldGetAddedSuccessfully(string bankAccount)
        {
            acctcreate.VerifyAccount(bankAccount);
        }

    }
}
