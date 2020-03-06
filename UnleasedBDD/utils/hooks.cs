using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using BoDi;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace UnleasedBDD.utils
{
    [Binding]
    class hooks
    {
        [assembly: log4net.Config.XmlConfigurator(Watch = true)]
        public string applicationURL= ConfigurationManager.AppSettings["testURL"];
        public IWebDriver driver;
        public ILog log;
        public static ExtentReports extent;
        public static ExtentTest test;



        [Before]
        public void launchURL()
        {
            extent = new ExtentReports();
            extent.AttachReporter(new ExtentHtmlReporter(TestContext.CurrentContext.TestDirectory + "\\extentReport.html"));
            var options = new ChromeOptions();
            options.AddArgument("incognito");
            options.AddArgument("--disable--dev--shm-usage");
            options.AddArgument("--no--sandbox");
            options.AddArgument("--disable--setuid--sandbox");
            driver = new ChromeDriver(options);
            log4net.Config.DOMConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            ScenarioContext.Current.Set<IWebDriver>(driver,"driver");
            ScenarioContext.Current.Set<ILog>(log, "log");
            driver.Navigate().GoToUrl(applicationURL);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            ScenarioContext.Current.Set<ExtentTest>(test, "extentTest");


        }
        [After]
        public void close()
        {
            driver.Close();
            driver.Quit();
            extent.Flush();
        }


    }
}
