using System;
using System.IO;
using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace MobileAutomation.Framework.Core.Utilities {
    public class Screenshot {
        /// <summary>
        /// Capture and attach screenshot on test failure.
        /// </summary>
        public static void Attach() {
            //Check if prereq passed and the file name/path setup correctly.
            string screenshotName = MobileTestContext.Get<string>(Constants.ScreenshotFileKey, false);
            if(string.IsNullOrEmpty(screenshotName)) {
                TestLogs.Write("No screenshot file exists, no screenshot will be attached.");
                return;
            }
            CaptureScreenshot(screenshotName);

            if(!File.Exists(screenshotName)) {
                TestLogs.Write("No screenshot file exists, no screenshot will be attached.");
                return;
            }
            TestContext.AddTestAttachment(MobileTestContext.Get<string>(Constants.ScreenshotFileKey), $"Error screenshot");
        }
        private static void CaptureScreenshot(string screenshotName) {
            try {
                MobileDriver.Instance.TakeScreenshot().SaveAsFile(screenshotName, ScreenshotImageFormat.Png);
            } catch(Exception e) {
                TestLogs.Write($"Cannot capture screenshot of the application. {e.InnerException}");
            }

        }

    }
}