using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace ElectionParser
{
    public static class Exstensions
    {

        public static bool ElementExsist(this IWebDriver webDriver, By Xpath)
        {
            try { return FindElements(webDriver, Xpath).Count > 0; }
            catch { return false; }
        }

        public static bool ElementExsist(this IWebElement webElement, By Xpath)
        {
            try { return FindElements(webElement, Xpath).Count > 0; }
            catch { return false; }
        }


        public static IWebElement FindElement(this IWebDriver webDriver, By Xpath)
        {
            Thread.Sleep(500);
            return webDriver.FindElement(Xpath);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver webDriver, By Xpath)
        {
            Thread.Sleep(500);
            return webDriver.FindElements(Xpath);
        }

        public static IWebElement FindElement(this IWebElement webElement, By Xpath)
        {
            Thread.Sleep(500);
            return webElement.FindElement(Xpath);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebElement webElement, By Xpath)
        {
            Thread.Sleep(500);
            return webElement.FindElements(Xpath);
        }

        public static void Click(this IWebElement webElement, int TimeOut, int milis)
        {
            try
            {
                webElement.Click();
                Thread.Sleep(milis);
            }
            catch
            {
                if (TimeOut > 0) Click(webElement, TimeOut - 1, milis);
            }
        }

        public static IWebElement TryFindElement(this IWebDriver driver, By Xpath)
        {
            try
            {
                return driver.FindElement(Xpath);
            }
            catch
            {
                return null;
            }
        }
            public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}

