using MobileAutomation.Framework.Core.BaseSetup;
using NUnit.Framework;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Utilities;
using OpenQA.Selenium.Appium.Enums;
using System.Collections.Generic;

namespace MobileAutomation.Framework.Core.Tests.UnitTests {
    [TestFixture]
    [Category("Unit Tests")]
    class DriverCapabilitiesTests {
        [SetUp]
        public void Setup() {
            MobileTestContext.Set(nameof(Constants.DevelopmentMode), "true");
        }
        [Test]
        public void ProvidingBrowserNameAndNoAppPackageForHybridAppThrowsForIOS() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.HYBRID,
                RS_BrowserName = MobileBrowserType.Safari,
                RS_DeviceGroup = "iOS;Tablet"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for hybrid apps");
            Assert.Throws<InvalidCapabilityException>(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAndAppPackageForHybridAppDoesNotThrowForIOS() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.HYBRID,
                RS_BrowserName = MobileBrowserType.Safari,
                RS_DeviceGroup = "iOS;Tablet",
                RS_AppPackage = "some.test.package",
                RS_AppActivity = ".SomeMainActivity"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for hybrid apps");
            Assert.DoesNotThrow(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAndNoAppPackageForHybridAppThrowsForAndroid() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.HYBRID,
                RS_BrowserName = MobileBrowserType.Chrome,
                RS_DeviceGroup = "Android;Tablet",
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for hybrid apps");
            Assert.Throws<InvalidCapabilityException>(() => caps.PreReqChecks(), "Prereq checks should throw");

        }
        [Test]
        public void ProvidingBrowserNameAndAppPackageForHybridAppDoesNotThrowForAndroid() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.HYBRID,
                RS_BrowserName = MobileBrowserType.Chrome,
                RS_DeviceGroup = "Android;Tablet",
                RS_AppPackage = "some.test.package",
                RS_AppActivity = ".SomeMainActivity"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for hybrid apps");
            Assert.DoesNotThrow(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAppPackageAndNoAppActivityForHybridAppThrowsForAndroid() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.HYBRID,
                RS_BrowserName = MobileBrowserType.Chrome,
                RS_DeviceGroup = "Android;Tablet",
                RS_AppPackage = "some.test.package",
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for hybrid apps");
            Assert.Throws<InvalidCapabilityException>(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAndNoAppPackageForNativeAppThrowsForIOS() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_BrowserName = MobileBrowserType.Safari,
                RS_DeviceGroup = "iOS;Tablet"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for native apps");
            Assert.Throws<InvalidCapabilityException>(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAndAppPackageForNativeAppDoesNotThrowForIOS() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_BrowserName = MobileBrowserType.Safari,
                RS_DeviceGroup = "iOS;Tablet",
                RS_AppPackage = "some.test.package",
                RS_AppActivity = ".SomeMainActivity"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for native apps");
            Assert.DoesNotThrow(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAndNoAppPackageForNativeAppThrowsForAndroid() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_BrowserName = MobileBrowserType.Chrome,
                RS_DeviceGroup = "Android;Tablet"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for native apps");
            Assert.Throws<InvalidCapabilityException>(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAndAppPackageForNativeAppDoesNotThrowForAndroid() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_BrowserName = MobileBrowserType.Chrome,
                RS_DeviceGroup = "Android;Tablet",
                RS_AppPackage = "some.test.package",
                RS_AppActivity = ".SomeMainActivity"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for native apps");
            Assert.DoesNotThrow(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void ProvidingBrowserNameAppPackageAndNoAppActivityForNativeAppThrowsForAndroid() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_BrowserName = MobileBrowserType.Chrome,
                RS_DeviceGroup = "Android;Tablet",
                RS_AppPackage = "some.test.package",
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.False(setOptions.ContainsKey("browserName"), "Browser name should be null for native apps");
            Assert.Throws<InvalidCapabilityException>(() => caps.PreReqChecks(), "Prereq checks should pass");

        }
        [Test]
        public void SettingBrowserOptionForWebAppPassesPrereqChecksInAndroid() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.WEB,
                RS_BrowserName = MobileBrowserType.Chrome,
                RS_DeviceGroup = "Android;Tablet"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.AreEqual(MobileBrowserType.Chrome, setOptions["browserName"], "Browser name should be set for web apps");

        }
        [Test]
        public void SettingBrowserOptionForWebAppPassesPrereqChecksInIOS() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.WEB,
                RS_BrowserName = MobileBrowserType.Safari,
                RS_DeviceGroup = "iOS;Tablet"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.AreEqual(MobileBrowserType.Safari, setOptions["browserName"], "Browser name should be set for web apps");

        }
        [Test]
        public void SettingDefaultAppPackageInNativeAppOnAndroidThrows() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_DeviceGroup = "Android;Tablet",
                RS_AppPackage = "io.android.testapp"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.Catch<InvalidCapabilityException>(() => caps.PreReqChecks(), "Invalid app name allowed to pass");
        }
        [Test]
        public void SettingDefaultAppActivityInHybridAppOnAndroidThrows() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_DeviceGroup = "Android;Tablet",
                RS_AppPackage = "some.random.app",
                RS_AppActivity = ".HomeScreenActivity"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.Catch<InvalidCapabilityException>(() => caps.PreReqChecks(), "Invalid app activity allowed to pass");
        }
        [Test]
        public void SettingDefaultAppPackageOnIOSThrows() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.NATIVE,
                RS_DeviceGroup = "iOS;Tablet",
                RS_AppPackage = "io.android.testapp"
            };

            DriverCapabilities caps = new DriverCapabilities(p);
            Dictionary<string, object> setOptions = caps.GetDriverOptions().ToDictionary();
            Assert.NotNull(caps.GetDriverOptions(), "Options should be set.");
            Assert.Catch<InvalidCapabilityException>(() => caps.PreReqChecks(), "Invalid app name allowed to pass");
        }
        [Test]
        public void EmptyApplicationTypeParameterThrows() {
            Assert.Throws<InvalidCapabilityException>(() => {
                DriverCapabilities caps = new DriverCapabilities(new TestEnvironmentParameters());
            }, "ApplicationType is a manadatory parameter and cannot be null or empty");
        }
        [Test]
        public void EmptyDeviceGroupParameterThrows() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.HYBRID
            };
            Assert.Throws<InvalidCapabilityException>(() => {
                DriverCapabilities caps = new DriverCapabilities(p);
            }, "DeviceGroup is a manadatory parameter and cannot be null or empty");
        }
        [Test]
        public void OverridingBrowserNameInWebAppReplacesDefaultCapability() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.WEB,
                RS_BrowserName = MobileBrowserType.Safari,
                RS_DeviceGroup = "iOS;Tablet"
            };
            DriverCapabilities caps = new DriverCapabilities(p);
            Assert.AreEqual(MobileBrowserType.Safari, caps.GetDriverOptions().ToDictionary()[MobileCapabilityType.BrowserName], "Default browser name should be set as capability.");
            AdditionalDriverOptions additionalOptions = new AdditionalDriverOptions();
            additionalOptions.AddCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Chrome);
            caps.SetAdditionalOptions(additionalOptions.GetCapabilities());
            Assert.AreEqual(MobileBrowserType.Chrome, caps.GetDriverOptions().ToDictionary()[MobileCapabilityType.BrowserName], "BrowserName capability should have been overridden.");
        }
        [Test]
        public void SpecifyingParametersWithRS_PrefixOverridesRunSettingsParameters() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.WEB,
                RS_BrowserName = MobileBrowserType.Safari,
                RS_DeviceGroup = "iOS;Tablet"
            };
            DriverCapabilities caps = new DriverCapabilities(p);
            Assert.AreEqual(MobileBrowserType.Safari, caps.GetDriverOptions().ToDictionary()[MobileCapabilityType.BrowserName], "Default browser name should be set as capability.");

            AdditionalDriverOptions additionalOptions = new AdditionalDriverOptions();
            additionalOptions
                .AddCapability("RS_DeviceGroup", "Android;phone")
                .AddCapability("RS_BrowserName", "Chrome");
            caps.MergeCapabilities(additionalOptions);
            Assert.AreEqual(p.RS_DeviceGroup, "Android;phone", "Device Group value should be overridden");
            Assert.AreEqual(p.RS_BrowserName, "Chrome", "Browser Name should be overridden");
            Assert.AreNotEqual(ApplicationType.NATIVE, p.RS_AppType, "Application Type runsettings parameter should not be overridden");


        }
        [Test]
        public void ProvidingDefaultOrNullSignInIdThrowsForIosNonWebApps() {
            TestEnvironmentParameters p = new TestEnvironmentParameters {
                RS_AppType = ApplicationType.HYBRID,
                RS_DeviceGroup = "iOS;Tablet"
            };
            DriverCapabilities caps = new DriverCapabilities(p);
            Assert.Throws<InvalidCapabilityException>(() => caps.MergeCapabilities(new AdditionalDriverOptions()), "Providing null signIn capability should throw.");
        }

    }
    

}

