using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Tests.TestUtilities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.Collections.Generic;

namespace MobileAutomation.Framework.Core.Tests.IntegrationTests {
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    [Category("Integration Tests")]
    public class GridBasedWebTests : DriverSetup {
        public override void FixtureSetup() {
            DefaultTestRunParameters.Get();
            base.FixtureSetup();

        }
        public override void FixtureTearDown() {
            TestRunParameters.Dispose();
            base.FixtureTearDown();
        }

        public override void TestSetup() {

            AdditionalCapabilities
               .AddCapability(nameof(Constants.RS_AppType), ApplicationType.WEB)
               .AddCapability(MobileCapabilityType.PlatformVersion, "10.0")
               .AddCapability(nameof(Constants.RS_BrowserName), "Chrome")
               .AddCapability("showChromedriverLog", "true")
               .AddCapability(nameof(Constants.RS_DeviceGroup), "Android;Phone;8_1;<yourDeviceId>;chrome")
               .AddCapability("uiautomator2ServerInstallTimeout", "40000");

            base.TestSetup();
        }


        [Test]
        [Category("Demo1")]
        public void CreateAndroidDriverInGridWithWebAppByDefaultPasses() {

            System.Console.WriteLine($"Thread # [{System.Threading.Thread.CurrentThread.ManagedThreadId}]");
            //Check if this is true for IOSdriver
            AndroidDriver<AppiumWebElement> driver = MobileDriver.Get<AndroidDriver<AppiumWebElement>>();
            // TestLogs.WriteDeviceLogs();
            ServerCommandExecutor executor = new ServerCommandExecutor(this.GetParameters().ServerUri, driver.SessionId);
            Console.WriteLine($"Current device orientation is:{driver.Orientation}");
            executor.Execute("setOrientation", new Dictionary<string, object> { { "orientation", "LANDSCAPE" } });
            Console.WriteLine($"New device orientation is: {driver.Orientation}");
            TestContext.WriteLine($"Web app SessionId is:[{driver.SessionId}]");
            Assert.NotNull(driver, $"driver cannot be null");
            /*System.Console.WriteLine($"Server running test [{driver.PlatformName}]");*/
            driver.Navigate().GoToUrl("http://google.com");
            System.Console.WriteLine($"Curretn activity:{driver.CurrentActivity}");

            Wait.ForPageToLoad();
            AppiumWebElement searchTextControl = driver.FindElement(By.CssSelector("div.SDkEP input.gLFyf"), 10);
            searchTextControl.Click();
            searchTextControl.SendKeys("Hello there@@");
            Wait.Seconds(3);
            Assert.AreEqual(driver.Contexts.Count, 2, "Multiple Contexts");
            Assert.AreEqual(driver.Context.ToString(), "CHROMIUM", "Expected Chromium context");
            OpenQA.Selenium.Remote.Response result1 = executor.Execute("getAvailableLogTypes");
            Console.WriteLine($"Supported Log Types:\n {result1.ToJson()}");


        }


    }
}