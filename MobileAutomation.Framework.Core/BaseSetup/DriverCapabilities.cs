using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
[assembly: InternalsVisibleTo("MobileAutomation.Framework.Core.Tests")]
namespace MobileAutomation.Framework.Core.BaseSetup {

    public class DriverCapabilities {

        private AppiumOptions options;
        private bool isNativeApp;
        private bool isHybridApp;
        private readonly TestEnvironmentParameters context;
        private bool isCapabilityRefreshNeeded;

        public DriverCapabilities(TestEnvironmentParameters context) {
            this.context = context;
            SetupBaseCapabilities();
        }
        private void SetupBaseCapabilities() {
            if(string.IsNullOrEmpty(this.context.RS_AppType.ToString())) {
                throw new InvalidCapabilityException("ApplicationType capability is mandatory cannot be null or empty");
            }
            this.isNativeApp = this.context.RS_AppType == ApplicationType.NATIVE;
            this.isHybridApp = this.context.RS_AppType == ApplicationType.HYBRID;
            if(string.IsNullOrEmpty(this.context.RS_DeviceGroup)) {
                throw new InvalidCapabilityException("DeviceGroup capability is mandatory cannot be null or empty");
            }
            this.options = SetBaseCapabilities();
        }
        private void OverrideRunSettingsParams(AdditionalDriverOptions driverOptions) {
            TestLogs.WriteLogSection("Overriding RunSettings",
                () => {
                    foreach(KeyValuePair<string, object> runSetting in driverOptions.GetCapabilities().Where(k => k.Key.Contains("RS_"))) {
                        System.Reflection.PropertyInfo contextProperty = this.context.GetType().GetProperty(runSetting.Key);
                        if(contextProperty != null && contextProperty.CanWrite) {
                            if(contextProperty.PropertyType == typeof(ApplicationType)) {
                                contextProperty.SetValue(this.context, (ApplicationType)Enum.Parse(typeof(ApplicationType), runSetting.Value.ToString()));
                                isCapabilityRefreshNeeded = true;
                                continue;
                            }
                            contextProperty.SetValue(this.context, runSetting.Value);
                            isCapabilityRefreshNeeded = true;
                            TestLogs.Write($"Overriding RunSettings Key: [{runSetting.Key}], New Value = [{runSetting.Value}]");

                        }
                    }
                });

            RefreshCapabilities();
        }
        private void RefreshCapabilities() {
            if(!isCapabilityRefreshNeeded) {
                return;
            }
            TestLogs.Write("Runsetting parameter change detected,Refreshing set capabilities now !!!");
            this.options = null;
            SetupBaseCapabilities();
        }
        public TestEnvironmentParameters GetEnvironmentContext() {
            return context;
        }
        /// <summary>
        /// Get all the set driver options.
        /// </summary>
        /// <returns></returns>
        public AppiumOptions GetDriverOptions() {
            return options;
        }

        /// <summary>
        /// Set additional capabilities on driver
        /// </summary>
        /// <param name="additionalCapability">key value pair for additional capabilities.</param>
        internal void SetAdditionalOptions(Dictionary<string, object> additionalCapability = null) {
            if(additionalCapability != null && additionalCapability.Count > 0) {
                //Log Section
                TestLogs.WriteLogSection("Test Specific Capabilities",
                    () => {
                        foreach(KeyValuePair<string, object> capability in additionalCapability) {
                            LogOverrideSetCapability(capability, options.ToDictionary());
                            options.AddAdditionalCapability(capability.Key, capability.Value);

                        }
                    });
            }
        }

        private void LogOverrideSetCapability(KeyValuePair<string, object> capability, Dictionary<string, object> existingCapabilities) {
            if(existingCapabilities.ContainsKey(capability.Key)) {
                TestLogs.Write($"Overriding Capability, Key:[{capability.Key}], OldValue:[{existingCapabilities[capability.Key]}], NewValue:[{capability.Value}]");
                return;
            }
            TestLogs.Write($"New Capability, Key:[{capability.Key}], Value:[{capability.Value}]");
        }

        // <summary>
        // Set Commandline arguments to be supplied for the browser. Only supported by chrome at the moment.
        // </summary>
        // <param name="arguments">arguments to add for the browser support</param>
        internal void SetArguments(params string[] arguments) {
            if(!this.context.RS_BrowserName.Equals(MobileBrowserType.Chrome)) {
                return;
            }
            
        }
        /// <summary>
        /// Sets up default browser capabilities
        /// </summary>
        /// <returns></returns>
        private AppiumOptions SetBaseCapabilities() {
            AppiumOptions cap = GetDefaultOptions();
            if(isNativeApp) {
                SetupNonWebAppOptions(cap);
                return cap;
            }
            if(isHybridApp) {
                SetupNonWebAppOptions(cap);
            }
            SetupWebAppOptions(this.context.RS_BrowserName, cap);
            return cap;

        }
        /// <summary>
        /// Set web app default options
        /// </summary>
        /// <param name="browserType">target browser type supported types(chrome, safari only)</param>
        /// <param name="options">existing appium options object to add capabilties</param>
        private void SetupWebAppOptions(string browserType, AppiumOptions options) {
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            switch(browserType) {
                case "Safari":
                    if(!isHybridApp) {
                        options.AddAdditionalCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Safari);
                    }
                    break;
                case "Chrome":
                    if(!isHybridApp) {
                        options.AddAdditionalCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Chrome);
                    }
                    
                    //Critical for extracting logs from chromium based browsers. Disable w3c to allow for JSONWP commands to be executed
                    options.AddAdditionalCapability("appium:chromeOptions", new Dictionary<string, object>() { { "w3c", false } });
                    break;
                default:
                    if(!isHybridApp && !isNativeApp) { //This is a  web app and browser is required
                        throw new InvalidCapabilityException($"Browser type [{this.context.RS_BrowserName}] is not supported. Supported types [chrome, safari]");
                    }
                    break;

            }
        }
        private AppiumOptions GetDefaultOptions() {
            //values from runsettings file
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion,
                context.RS_PlatformVersion);
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName,
                context.RS_PlatformName);
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName,
                context.RS_DeviceName);
            capabilities.AddAdditionalCapability(MobileCapabilityType.App,
                context.RS_AppPackagePath);
            capabilities.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout,
                context.RS_NewCommandTimeout);
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, "false");
            capabilities.AddAdditionalCapability(MobileCapabilityType.NoReset, "true");
            capabilities.AddAdditionalCapability("clearDeviceLogsOnStart", "true");
            capabilities.AddAdditionalCapability(Constants.RS_DeviceGroup, context.RS_DeviceGroup);
            return capabilities;
        }
        private DeviceType GetDeviceType() {
            string tablet = "Tablet";
            string phone = "Phone";
            string androidOS = "Android";
            string ios = "iOS";
            string deviceGroup = this.context.RS_DeviceGroup;
            if(deviceGroup.Contains(androidOS, StringComparison.OrdinalIgnoreCase)) {
                if(deviceGroup.Contains(tablet,StringComparison.OrdinalIgnoreCase)) {
                    return DeviceType.ANDROIDTABLET;
                }
                if(deviceGroup.Contains(phone, StringComparison.OrdinalIgnoreCase)) {
                    return DeviceType.ANDROIDPHONE;
                }
                throw new InvalidCapabilityException($"Android Device type specified in [{nameof(deviceGroup)} = {deviceGroup}] is not supported! Supported options:[{tablet},{phone}]");
            }
            if(deviceGroup.Contains(ios, StringComparison.OrdinalIgnoreCase)) {
                if(deviceGroup.Contains(tablet)) {
                    return DeviceType.IOSTABLET;
                }
                if(this.context.RS_DeviceGroup.Contains(phone, StringComparison.OrdinalIgnoreCase)) {
                    return DeviceType.IOSPHONE;
                }
                throw new InvalidCapabilityException($"iOS Device type specified in [{nameof(deviceGroup)} = {deviceGroup}] is not supported! Supported options:[{tablet},{phone}]");
            }
            throw new InvalidCapabilityException($"Device type specified in [{nameof(deviceGroup) }={deviceGroup}] is not supported! Supported OS:[{androidOS},{ios}], and device type [{tablet},{phone}]");
        }
        /// <summary>
        /// Set Native App cofigurations.
        /// </summary>
        /// <returns></returns>
        private void SetupNonWebAppOptions(AppiumOptions nonWebOptions) {
            if(GetDeviceType() == DeviceType.ANDROIDPHONE || GetDeviceType() == DeviceType.ANDROIDTABLET) {
                nonWebOptions.PlatformName = "Android";
                nonWebOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
                nonWebOptions.AddAdditionalCapability(AndroidMobileCapabilityType.IgnoreUnimportantViews, "");

                nonWebOptions.AddAdditionalCapability(AndroidMobileCapabilityType.DeviceReadyTimeout,
                    Convert.ToInt32(context.RS_DeviceReadyTimeout));
                nonWebOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity,
                    context.RS_AppActivity);
                nonWebOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage,
                    context.RS_AppPackage);

                return;
            }

            if(GetDeviceType() == DeviceType.IOSPHONE || GetDeviceType() == DeviceType.IOSTABLET) {
                nonWebOptions.PlatformName = "iOS";
                nonWebOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
                nonWebOptions.AddAdditionalCapability(IOSMobileCapabilityType.LaunchTimeout, Convert.ToInt32(context.RS_DeviceReadyTimeout));
                nonWebOptions.AddAdditionalCapability(IOSMobileCapabilityType.BundleId, context.RS_AppPackage);
                nonWebOptions.AddAdditionalCapability("fullContextList", "true");
                return;
            }

        }
        /// <summary>
        /// Merge additional capabilities supplied by the user
        /// along with the default capabilities. .see AdditionalDriverOptions
        /// </summary>
        /// <param name="options">.seealso AdditionalDriverOptions</param>
        public void MergeCapabilities(AdditionalDriverOptions options) {
            if(options == null) {
                return;
            }
            //Overriding RunSettings
            if(options.GetCapabilities().Any(k => k.Key.Contains("RS_"))) {
                //Regenerate Driver Capabilities
                OverrideRunSettingsParams(options);

            }
            SetAdditionalOptions(options.GetCapabilities().Where(c => !c.Key.Contains("RS_")).ToDictionary(x => x.Key, x => x.Value));
            if(options.GetArguments().Count > 0) {
                SetArguments(options.GetArguments().ToArray());
            }

            PreReqChecks();
        }
        /// <summary>
        /// Basic checks since all capabilities have been set
        /// </summary>
        internal void PreReqChecks() {
            Dictionary<string, object> caps = options.ToDictionary();
            object values;
            TestLogs.WriteLogSection("Prerequisite Checks", () => {
                TestLogs.Write("Starting checks...");
                if(this.isNativeApp || this.isHybridApp) {
                    //Browser should be null
                    if(caps.TryGetValue(MobileCapabilityType.BrowserName, out values)) {
                        if(!Helper.IsNullOrEmpty(values.ToString(), false)) {
                            throw new InvalidCapabilityException($"Capability combination: [{MobileCapabilityType.BrowserName}] = [{values?.ToString()}] is invalid for application type [{this.context.RS_AppType}]");
                        }

                    }
                    values = null;
                    //Check App package or AppActivity is set or not
                    if(this.context.RS_DeviceGroup.Contains("ANDROID", StringComparison.OrdinalIgnoreCase)) {
                        if(caps.TryGetValue(AndroidMobileCapabilityType.AppPackage, out values)) {
                            if(values == null || values.Equals("io.android.testapp")) {
                                throw new InvalidCapabilityException($"Capability combination: [{AndroidMobileCapabilityType.AppPackage}] = [{values?.ToString()}] is invalid for application type [{this.context.RS_AppType}]");
                            }
                        }
                        values = null;

                        if(caps.TryGetValue(AndroidMobileCapabilityType.AppActivity, out values)) {
                            if(values == null || values.Equals(".HomeScreenActivity")) {
                                throw new InvalidCapabilityException($"Capability combination: [{AndroidMobileCapabilityType.AppActivity}] = [{values?.ToString()}] is invalid for application type [{this.context.RS_AppType}]");
                            }
                        }
                        values = null;

                    }
                    if(this.context.RS_DeviceGroup.Contains("IOS",StringComparison.OrdinalIgnoreCase)) {
                        if(caps.TryGetValue(IOSMobileCapabilityType.BundleId, out values)) {
                            if(values == null || values.Equals("io.android.testapp")) {
                                throw new InvalidCapabilityException($"Capability combination: [{IOSMobileCapabilityType.AppName}] = [{values?.ToString()}] is invalid for application type [{this.context.RS_AppType}]");
                            }
                        }
                        

                    }
                    values = null;

                } else { //Web app
                    //Browser should not be null
                    if(caps.TryGetValue(MobileCapabilityType.BrowserName, out values)) {
                        if(string.IsNullOrEmpty(values.ToString())) {
                            throw new InvalidCapabilityException($"Capability combination: [{MobileCapabilityType.BrowserName}] = [{values?.ToString()}] is invalid for application type [{this.context.RS_AppType}]");
                        }

                    }
                }
                TestLogs.Write("No issues found!!");
            });

        }

    }

    
}