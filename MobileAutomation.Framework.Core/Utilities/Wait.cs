using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace MobileAutomation.Framework.Core.Utilities {
    public class Wait {
        /// <summary>
        /// wait for zillion seconds.
        /// </summary>
        /// <param name="seconds"></param>
        public static void Seconds(double seconds) {
            int waitMs = (int)(seconds * 1000);
            Thread.Sleep(waitMs);
        }

        public static void UntilElementNotFound(By by) {
            UntilNotVisible(by);
        }
        /// <summary>
        /// Wait until element not found
        /// </summary>
        /// <param name="by"></param>
        /// <param name="waitTime"></param>
        public static void UntilElementNotFound(By by, int waitTime) {
            UntilNotVisible(by, waitTime);
        }
        /// <summary>
        /// Wait until element is invisible
        /// </summary>
        /// <param name="by">search criteria for finding element</param>
        /// <param name="timeoutInSeconds">timeout for element to disapper</param>
        public static void UntilNotVisible(By by, int timeoutInSeconds = Constants.DefaultWaitTime) {
            UntilNotVisible(by, timeoutInSeconds);
        }


        /// <summary>
        /// Wait until all elements are invisible within timeout
        /// </summary>
        /// <param name="by">element selector</param>
        /// <param name="waitTimeInSeconds">timeout in seconds</param>
        public static void UntilInvisibilityOfAllElements(By by, int timeoutInSeconds = Constants.DefaultWaitTime) {
            WebDriverWait wait = new WebDriverWait(MobileDriver.Instance, TimeSpan.FromSeconds(timeoutInSeconds)) {
                Timeout = new TimeSpan(0, 0, timeoutInSeconds),
            };
            try {
                wait.Until(driver => driver.FindElements(by).All(e => !e.Displayed));
            } catch(WebDriverTimeoutException) {
                throw new AssertionException($"Element was continually found when waiting for lack of visibility via: {by}");
            }
        }
        /// <summary>
        /// Wait until element is visible
        /// </summary>
        /// <param name="by">search criteria for finding element</param>
        /// <param name="timeoutInSeconds">timeout for element visibility</param>
        public static void UntilVisible(By by, int timeoutInSeconds = Constants.DefaultWaitTime) {
            WebDriverWait wait = new WebDriverWait(MobileDriver.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            try {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            } catch(Exception e) {
                throw new AssertionException($"Element was not found when waiting for visibility via: {by} for {timeoutInSeconds} seconds.\n Exceptions: {e}");
            }

        }
        
        /// <summary>
        /// Wait for appearTime for element to appear, incase it takes an unusual amount of time.
        /// If element still not seen, assume it has appeared and disappeard and all is well.
        /// if element does appear, store its status and wait for it to disappear.
        /// </summary>
        /// <param name="by">condition to find element</param>
        /// <param name="appearTime">tiemout for element appearing</param>
        /// <param name="disappearTime">timeout for element disappearing</param>
        /// <param name="pollingInterval">retry interval</param>
        /// <param name="throwException">flag indicating whether exception to be thrown if element did not DISAPPEAR within disappearTime timeout. Defaults to false. </param>
        public static void UntilVisibleThenNotVisible(By by, int appearTime = Constants.DefaultWaitTime, int disappearTime = Constants.DefaultWaitTime, double pollingInterval = 0.1, bool throwException = false) {
            IWebElement element;
            try {
                WebDriverWait wait = new WebDriverWait(new SystemClock(), MobileDriver.Instance, TimeSpan.FromSeconds(appearTime), TimeSpan.FromSeconds(pollingInterval));
                element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            } catch(WebDriverTimeoutException) {
                TestContext.WriteLine($"Element with selector[{by}] still not visible after [{appearTime}]");
                return;
            }

            if(element == null) {
                return;
            }
            UntilNotVisible(by, disappearTime, throwException);
        }
        /// <summary>
        /// Wait untile element is not visible
        /// </summary>
        /// <param name="by">condition to search element</param>
        /// <param name="waitTime">time to search element for</param>
        /// <param name="throwException">flag to allow throwing exception when element not found within waitTime, defaults to true.</param>
        public static void UntilNotVisible(By by, int waitTime, bool throwException = true) {
            WebDriverWait wait = new WebDriverWait(MobileDriver.Instance, TimeSpan.FromSeconds(waitTime)) {
                Timeout = new TimeSpan(0, 0, waitTime),
            };
            try {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            } catch(WebDriverTimeoutException) {
                if(throwException) {
                    throw new AssertionException(
                        $"Element was continually found when waiting for lack of visibility via: {by}");
                }
                TestContext.WriteLine($"Element with selector[{by}] still visible after [{waitTime}], no exception thrown as {nameof(throwException)} flag is [{throwException}]");
            }
        }


        /// <summary>
        /// Retry an action until condition satisfied or until timeout.
        /// </summary>
        /// <param name="function">action</param>
        /// <param name="maxWaitTime">timeout</param>
        public static void For(Action function, int maxWaitTime = Constants.DefaultWaitTime) {
            RetryHelper.Retry(function, maxWaitTime);
        }
        /// <summary>
        /// Retry an action until condition satisfied or until timeout.
        /// </summary>
        /// <param name="function">action</param>
        /// <param name="maxWaitTime">timout</param>
        public static void For(Func<bool> function, int maxWaitTime = Constants.DefaultWaitTime) {
            RetryHelper.Retry(function, maxWaitTime);
        }
        /// <summary>
        /// Wait for the page ready state to be complete
        /// </summary>
        /// <param name="timeoutInSeconds">timeout, default 5 seconds</param>
        public static void ForPageToLoad(int timeoutInSeconds = Constants.DefaultWaitTime) {
            Stopwatch watch = new Stopwatch();
            watch.Restart();
            while(MobileDriver.Instance.ExecuteScript("return document.readyState;").ToString() != "complete") {
                if(watch.Elapsed.TotalSeconds > timeoutInSeconds) {
                    break;
                }
                Wait.Seconds(0.5);
            }

            if(MobileDriver.Instance.ExecuteScript("return document.readyState;").ToString() != "complete") {
                throw new InvalidElementStateException($"Page did not load successfully in [{timeoutInSeconds}] seconds");
            }
        }
    }
}