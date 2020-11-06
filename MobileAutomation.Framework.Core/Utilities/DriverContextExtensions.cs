using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MobileAutomation.Framework.Core.Defaults;
using OpenQA.Selenium.Appium;

namespace MobileAutomation.Framework.Core.Utilities {
    public static class DriverContextExtensions {
        /// <summary>
        /// Switch web view context
        /// </summary>
        /// <param name="driver">appium driver</param>
        /// <param name="condition">condition to switch to a specific web view</param>
        ///<param name="timeoutInSeconds">timeout to wait for the condition to be satisfied</param>
       /* public static void SwitchWebViewContext(this AppiumDriver<AppiumWebElement> driver, Func<bool> condition, int timeoutInSeconds = Constants.DefaultWaitTime) {
            ReadOnlyCollection<string> availableWindowHandles;
            Stopwatch watch = new Stopwatch();
            watch.Restart();
            bool isFound = false;
            do {
                availableWindowHandles = driver.WindowHandles;
                foreach(string context in availableWindowHandles) {
                    driver.SwitchTo().Window(context);
                    if(condition()) {
                        isFound = true;
                        break;
                    }
                }

                Wait.Seconds(0.5);
            } while((watch.ElapsedMilliseconds / 1000) < timeoutInSeconds && !isFound);
            if(!isFound) {
                throw new Exception("Cannot switch webView context!!");
            }
        }*/
        /// <summary>
        /// Switch driver context to native or webviews
        /// </summary>
        /// <param name="driver">appium driver</param>
        /// <param name="contextToSwitchTo">context to switch to</param>
        public static void SwitchContextTo(this AppiumDriver<AppiumWebElement> driver, string contextToSwitchTo) {
            ReadOnlyCollection<string> availableContexts;
            availableContexts = driver.Contexts;
            bool isFound = false;
            foreach(string context in availableContexts) {
                if(context.Contains(contextToSwitchTo,StringComparison.OrdinalIgnoreCase)) {
                    driver.Context = context;
                    isFound = true;
                    break;
                }
            }
            if(!isFound) {
                throw new Exception($"Cannot switch context to {contextToSwitchTo}!!");
            }
        }

        public static bool SwitchContextTo(this AppiumDriver<AppiumWebElement> driver ,KeyValuePair<string,string> searchCriteria = default, string contextType = "webview", int timeout = 10) {
            bool isFound = false;
            if(searchCriteria.Equals(default(KeyValuePair<string, string>))){
                throw new ArgumentOutOfRangeException($"[{nameof(searchCriteria)}] is mandatory and cannot be null.");
            }
            ValidateContextLoaded(driver, contextType, timeout);
            var response = driver.ExecuteScript("mobile:getContexts");
            ReadOnlyCollection<object> contextObjects = response as ReadOnlyCollection<object>;
            foreach(object context in contextObjects) {
                var contextProps = context as Dictionary<string, object>;
                object value;
                if(!contextProps.TryGetValue(searchCriteria.Key, out value)) {
                    continue;
                }
                if(value != null && value.ToString().Contains(searchCriteria.Value, StringComparison.OrdinalIgnoreCase)) {
                    object contextId;
                    if(!contextProps.TryGetValue("id", out contextId)) {
                        throw new Exception("No Appropriate Context found to switch to.");
                    }
                    driver.Context = contextId.ToString();
                    isFound = true;
                }

            }
            return isFound;
        }

        private static void ValidateContextLoaded(AppiumDriver<AppiumWebElement> driver, string contextToSwitchTo, int retryCount) {
            if(retryCount >= 0) {
                for(int i = 0; i < retryCount - 1; i++) {
                    var availableContexts = driver.Contexts;
                    if(availableContexts.Any(c => c.Contains(contextToSwitchTo, StringComparison.OrdinalIgnoreCase))) {
                        break;
                    }
                    Wait.Seconds(1);
                }
            }
        }
    }
}