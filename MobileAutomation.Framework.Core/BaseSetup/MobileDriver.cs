using System;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace MobileAutomation.Framework.Core.BaseSetup {
    public class MobileDriver {
        public static AppiumDriver<AppiumWebElement> Instance {
            get {
                
                return MobileTestContext.Get<AppiumDriver<AppiumWebElement>>(nameof(MobileDriver));
            }
            set {

                MobileTestContext.Set(nameof(MobileDriver), value);
            }
        }
        
        /// <summary>
        /// Gets the driver instance based on the type provided, must be
        /// AppiumDriver types
        /// </summary>
        /// <typeparam name="T">Instance of type AppiumDriver</typeparam>
        public static T Get<T>() where T : class {
            T result = default(T);
            string expectedPlatform = Instance.Capabilities.GetCapability("platformName").ToString();
            result = Instance as T;
            if(result != null) {
                return result;
            }
            throw new InvalidCastException($"Cannot convert [{nameof(Instance)}] with platform [{expectedPlatform}] to [{typeof(T).Name}].");
        }

        /// <summary>
        /// Initializes the Appium driver. Service can be set using .see AppiumServer 
        /// for local execution and by setting the .seealso TestRunParameters.LocalExecution,
        /// nodeEXE and AppiumJS path in runsettings file. Set AppiumServerUri for grid or manual server test execution. 
        /// To get the instance of the appropriate driver, use the .see "GetInstance" method
        /// </summary>
        /// <param name="capabilities">Capabilities expected of the driver</param>
        public static void Initialize(DriverCapabilities capabilities) {
            AppiumDriver<AppiumWebElement> instance;
            TestEnvironmentParameters TestRunParameters = capabilities.GetEnvironmentContext();
            TimeSpan implicitWaitTimeSpan = TimeSpan.FromSeconds(Convert.ToInt32(TestRunParameters.RS_ImplicitWaitTime));
            instance = InitializeDriver(capabilities); 
            instance.Manage().Timeouts().ImplicitWait = implicitWaitTimeSpan;
            Instance = instance;
        }

        private static AppiumDriver<AppiumWebElement> InitializeDriver(DriverCapabilities capabilities) {
            AppiumOptions caps = capabilities.GetDriverOptions();
            TestEnvironmentParameters TestRunParameters = capabilities.GetEnvironmentContext();
            int timeout = Convert.ToInt32(TestRunParameters.RS_DeviceReadyTimeout);

            Uri serverUri = TestRunParameters.ServerUri;
            if(TestRunParameters.RS_PlatformName.Equals("Android",StringComparison.OrdinalIgnoreCase)) {
                return GetDriver(() => new CustomAndroidDriver(serverUri, caps, TimeSpan.FromSeconds(timeout)));
            }
            if(TestRunParameters.RS_PlatformName.Equals("iOS", StringComparison.OrdinalIgnoreCase)) {
                return GetDriver(() => new CustomIOSDriver(serverUri, caps, TimeSpan.FromSeconds(timeout)));
            }
            throw new PlatformNotSupportedException($"[{TestRunParameters.RS_PlatformName}] platform is not supported.");

        }

        /// <summary>
        /// Exception Wrapper creating driver
        /// </summary>
        /// <param name="method">method to execute inside the wrapper</param>
        /// <returns></returns>
        private static AppiumDriver<AppiumWebElement> GetDriver(Func<AppiumDriver<AppiumWebElement>> method) {
            try {
                return method();
            } catch(Exception e) {
                TestLogs.Write(e.ToString());
                throw new WebDriverException($"Cannot Create Driver Instance. Exception:\n{e}");
            }

        }

        public static void Dispose() {
            try {
                if(Instance != null) {
                    Instance.Quit(); //Calls driver dispose behind the scenes.
                }
                MobileTestContext.Dispose();
            } catch(Exception ex) {
                TestContext.WriteLine($"Failed to disposed off driver session: {ex.Message}");
            }
        }

    }

}