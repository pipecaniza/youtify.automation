using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Youtify.Automation.Extensions;

namespace Youtify.Automation.PageObjects
{
    public class GoogleAuthPageObject : IDisposable
    {
        protected readonly ChromeDriver _driver;

        public IWebElement EmailField
        {
            get => _driver.WaitFor("#identifierId");
        }

        public IWebElement EmailNextButton
        {
            get => _driver.WaitFor("#identifierNext");
        }

        public IWebElement PasswordField
        {
            get => _driver.WaitFor("input[name=password]");
        }

        public IWebElement PasswordNextButton
        {
            get => _driver.WaitFor("#passwordNext");
        }


        public GoogleAuthPageObject(ChromeDriver chromeDriver)
        {
            _driver = chromeDriver;
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
        }

        public void Dispose()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles.First());
        }

        public void Login(string email, string password)
        {
            EmailField.SendKeys(email);
            EmailNextButton.Click();
            PasswordField.SendKeys(password);
            PasswordNextButton.Click();
        }
    }
}
