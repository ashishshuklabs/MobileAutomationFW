using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using System;

namespace MobileAutomation.Framework.Core.BaseSetup {
    public class EnvironmentConfiguration {
        private string RS_BrowserName { get; }
        private string RS_AppType { get; }
        private string ImplicitWaitTime { get; }
        private string RS_DeviceName { get; }
        private string RS_AppActivity { get; }
        private string RS_AppPackagePath { get; }
        private string RS_AppPackage { get; }
        private string RS_AppPassword { get; }
        private string RS_PlatformName { get; }
        private string RS_NewCommandTimeout { get; }
        private string RS_DeviceReadyTimeout { get; }
        private string RS_PlatformVersion { get; }
        private string RS_ServerHost { get; }
        private string RS_ServerResource { get; }

        private string RS_ServerPort { get; }
        private string RS_NodeExePath { get; }
        private string RS_AppiumJSPath { get; }
        private string RS_AutoDownloadChromeDriver { get; }
        private string RS_LocalExecutionAsService { get; }
        private string RS_LocalExecution { get; }
        private string RS_DeviceGroup { get; }
        public EnvironmentConfiguration() {
            this.RS_BrowserName = TestRunParameters.Get(nameof(Constants.RS_BrowserName)) ?? Constants.RS_BrowserName;
            this.RS_AppType = TestRunParameters.Get(nameof(Constants.RS_AppType)) ?? Constants.RS_AppType;
            this.ImplicitWaitTime = TestRunParameters.Get(nameof(Constants.RS_ImplicitWaitTime)) ?? Constants.RS_ImplicitWaitTime;
            this.RS_DeviceName = TestRunParameters.Get(nameof(Constants.RS_DeviceName)) ?? Constants.RS_DeviceName;
            this.RS_AppActivity = TestRunParameters.Get(nameof(Constants.RS_AppActivity)) ?? Constants.RS_AppActivity;
            this.RS_AppPackagePath = TestRunParameters.Get(nameof(Constants.RS_AppPackagePath)) ?? Constants.RS_AppPackagePath;
            this.RS_AppPackage = TestRunParameters.Get(nameof(Constants.RS_AppPackage)) ?? Constants.RS_AppPackage;
            this.RS_AppPassword = TestRunParameters.Get(nameof(Constants.RS_AppPassword)) ?? Constants.RS_AppPassword;
            this.RS_ServerPort = TestRunParameters.Get(nameof(Constants.RS_ServerPort)) ?? Constants.RS_ServerPort;
            this.RS_PlatformName = TestRunParameters.Get(nameof(Constants.RS_PlatformName)) ?? Constants.RS_PlatformName;
            this.RS_NewCommandTimeout = TestRunParameters.Get(nameof(Constants.RS_NewCommandTimeout)) ?? Constants.RS_NewCommandTimeout;
            this.RS_DeviceReadyTimeout = TestRunParameters.Get(nameof(Constants.RS_DeviceReadyTimeout)) ?? Constants.RS_DeviceReadyTimeout;
            this.RS_PlatformVersion = TestRunParameters.Get(nameof(Constants.RS_PlatformVersion)) ?? Constants.RS_PlatformVersion;
            this.RS_ServerHost = TestRunParameters.Get(nameof(Constants.RS_ServerHost)) ?? Constants.RS_ServerHost;
            this.RS_ServerResource = TestRunParameters.Get(nameof(Constants.RS_ServerResource)) ?? Constants.RS_ServerResource;
            this.RS_DeviceGroup = TestRunParameters.Get(nameof(Constants.RS_DeviceGroup)) ?? Constants.RS_DeviceGroup;
            
            this.RS_NodeExePath = TestRunParameters.Get(nameof(Constants.RS_NodeExePath)) ?? Constants.RS_NodeExePath;
            this.RS_AppiumJSPath = TestRunParameters.Get(nameof(Constants.RS_AppiumJSPath)) ?? Constants.RS_AppiumJSPath;
            this.RS_AutoDownloadChromeDriver = TestRunParameters.Get(nameof(Constants.RS_AutoDownloadChromeDriver)) ?? Constants.RS_AutoDownloadChromeDriver;
            this.RS_LocalExecutionAsService = TestRunParameters.Get(nameof(Constants.RS_LocalExecutionAsService)) ?? Constants.RS_LocalExecutionAsService;
            this.RS_LocalExecution = TestRunParameters.Get(nameof(Constants.RS_LocalExecution)) ?? Constants.RS_LocalExecution;
        }

        /// <summary>
        /// Get Environment Details upon reading runsettings file
        /// EnvironmentContext and prefixed setting on default.
        /// </summary>
        /// <returns></returns>
        public TestEnvironmentParameters Get() {
            return new TestEnvironmentParameters {
                RS_AppType = Helper.SetApplicationType(this.RS_AppType),
                RS_BrowserName = Helper.SetBrowserType(this.RS_BrowserName),
                ServerUri = Helper.CreateUri(RS_ServerHost, RS_ServerPort, RS_ServerResource),
                RS_ImplicitWaitTime = this.ImplicitWaitTime,
                RS_DeviceName = this.RS_DeviceName,
                RS_AppActivity = this.RS_AppActivity,
                RS_AppPackagePath = this.RS_AppPackagePath,
                RS_AppPackage = this.RS_AppPackage,
                RS_AppPassword = this.RS_AppPassword,
                RS_ServerPort = this.RS_ServerPort,
                RS_PlatformName = this.RS_PlatformName,
                RS_NewCommandTimeout = this.RS_NewCommandTimeout,
                RS_DeviceReadyTimeout = this.RS_DeviceReadyTimeout,
                RS_PlatformVersion = this.RS_PlatformVersion,
                RS_NodeExePath = this.RS_NodeExePath,
                RS_AppiumJSPath = this.RS_AppiumJSPath,
                RS_AutoDownloadChromeDriver = this.RS_AutoDownloadChromeDriver,
                RS_LocalExecutionAsService = this.RS_LocalExecutionAsService,
                RS_ServerHost = this.RS_ServerHost,
                RS_LocalExecution = this.RS_LocalExecution,
                RS_DeviceGroup = this.RS_DeviceGroup
                
            };
        }

    }
}