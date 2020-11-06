using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Utilities;
using System;

namespace MobileAutomation.Framework.Core.Tests.TestUtilities {
    public class DefaultTestRunParameters {
        
        private static void SetDevModeDefaultParams() {
            TestRunParameters.isDevelopmentMode = true;
            TestRunParameters.Set(nameof(Constants.RS_LocalExecutionAsService), "false");
            TestRunParameters.Set(nameof(Constants.RS_LocalExecution), "true");
            TestRunParameters.Set(nameof(Constants.RS_AppType), ApplicationType.WEB.ToString());
            TestRunParameters.Set(nameof(Constants.RS_ServerHost), Constants.RS_ServerHost);
            TestRunParameters.Set(nameof(Constants.RS_ServerPort), Constants.RS_ServerPort);
            TestRunParameters.Set(nameof(Constants.RS_ServerResource), Constants.RS_ServerResource);
            TestRunParameters.Set(nameof(Constants.RS_AppiumJSPath), Constants.RS_AppiumJSPath);
            TestRunParameters.Set(nameof(Constants.RS_NodeExePath), Constants.RS_NodeExePath);
            TestRunParameters.Set(nameof(Constants.RS_PlatformName), Constants.RS_PlatformName);
            TestRunParameters.Set(nameof(Constants.RS_NewCommandTimeout), Constants.RS_NewCommandTimeout);
            TestRunParameters.Set(nameof(Constants.RS_ImplicitWaitTime), Constants.RS_ImplicitWaitTime);
            TestRunParameters.Set(nameof(Constants.RS_DeviceReadyTimeout), Constants.RS_DeviceReadyTimeout);
            TestRunParameters.Set(nameof(Constants.RS_AppActivity), Constants.RS_AppActivity);
            TestRunParameters.Set(nameof(Constants.RS_AppPackage), Constants.RS_AppPackage);
            TestRunParameters.Set(nameof(Constants.RS_AppPackagePath), Constants.RS_AppPackagePath);
            TestRunParameters.Set(nameof(Constants.RS_AppPassword), Constants.RS_AppPassword);
            TestRunParameters.Set(nameof(Constants.RS_BrowserName), Constants.RS_BrowserName);
            TestRunParameters.Set(nameof(Constants.RS_DeviceName), Constants.RS_DeviceName);
            TestRunParameters.Set(nameof(Constants.RS_PlatformVersion), Constants.RS_PlatformVersion);
            TestRunParameters.Set(nameof(Constants.RS_AutoDownloadChromeDriver), Constants.RS_AutoDownloadChromeDriver);

            TestRunParameters.Set(nameof(Constants.RS_DeviceGroup), Constants.RS_DeviceGroup);
            //TestRunParameters.Set(nameof(Constants.RS_XcodeSigninId), Constants.RS_XcodeSigninId);

        }
        /// <summary>
        /// Get Test Run Parameters. Ensure you call EnvironmentConfiguration's Get method again after this step
        /// for the changes to take effect in FW tests..
        /// </summary>
        public static void Get() {
            SetDevModeDefaultParams();
        }
        /// <summary>
        /// Set/override default test run parameters.
        /// </summary>
        /// <param name="key">parameter key</param>
        /// <param name="value">parameter value</param>
        public static void Set(string key, string value) {
            if(!TestRunParameters.isDevelopmentMode) {
                throw new InvalidOperationException("Cannot update when default values not set. Call Get method first(and only once) before updating default values.");
            }
            if(string.IsNullOrEmpty(key)) {
                return;
            }
            TestRunParameters.Set(key, value, true);
        }
    }
}
