using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace UnleasedBDD.utils
{
    [Binding]
    class OtherUtils
    {
        private IWebDriver driver;

        public OtherUtils(IWebDriver driver)
        {
            this.driver = driver;

        }

        public bool isElementPresent(IWebElement alertText)
        {
            Boolean foundAlert = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            try
            {
                wait.Until(driver => alertText.Displayed);
                foundAlert = true;
            }
            catch (Exception ex)
            {
                foundAlert = false;
            }
            return foundAlert;
        }

    }
}
