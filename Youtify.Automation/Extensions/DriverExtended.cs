using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using Youtify.Automation.Common;

namespace Youtify.Automation.Extensions
{
    static class DriverExtended
    {
        static public IWebElement WaitFor(
            this ChromeDriver driver, string selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Globals.MAX_TIMEOUT_SECS));
            var result = wait.Until(
                page => page.FindElements(
                    By.CssSelector(selector)
                ).FirstOrDefault(e => e.IsVisible())
            );
            return result;
        }

        static public IWebElement WaitFor(
            this ChromeDriver driver, string selector, Func<IWebElement, bool> filter)
        {       
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Globals.MAX_TIMEOUT_SECS));
            var result = wait.Until(
                page => page.FindElements(
                    By.CssSelector(selector)
                ).FirstOrDefault(filter)
            );
            return result;            
        }

        static public void WaitUntilDisapear(
            this ChromeDriver driver, string selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Globals.MAX_TIMEOUT_SECS));
            var result = wait.Until(
                page => page.FindElements(
                    By.CssSelector(selector)
                ).Count(e => e.IsVisible()) == 0
            );
        }

        static public bool IsVisible(this IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }
    }
}
