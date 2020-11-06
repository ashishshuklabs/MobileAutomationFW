using System;
using System.Collections.Generic;
using System.IO;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace MobileAutomation.Framework.Core.BaseSetup {
    public class AppiumServer {
        /// <summary>
        /// Start Appium Server for local execution. Updates the ref parameter <seealso cref=" TestEnvironmentParameters.ServerUri"/> accordingly and sets up the MobileTestContext with parameter with key <see cref="Constants.AppiumServiceKey"/> of type AppiumLocalService
        /// </summary>
        /// <param name="nodeExePath"></param>
        /// <param name="appiumJSPath"></param>
        public static void Start(TestEnvironmentParameters parameters) {
            if(!Convert.ToBoolean(parameters.RS_LocalExecution)) {
                return;
            }
            if(!Convert.ToBoolean(parameters.RS_LocalExecutionAsService)) {
                return;
            }
            if(string.IsNullOrEmpty(parameters.RS_NodeExePath)) {
                throw new ArgumentNullException($" [{nameof(parameters.RS_NodeExePath)}] is mandatory for local execution");
            }
            if(string.IsNullOrEmpty(parameters.RS_AppiumJSPath)) {
                throw new ArgumentNullException($" [{nameof(parameters.RS_AppiumJSPath)}] is mandatory for local execution");
            }
            if(string.IsNullOrEmpty(parameters.RS_ServerHost)) {
                throw new ArgumentNullException($" [{nameof(parameters.RS_ServerHost)}] is mandatory for local execution");
            }
            string nodeExePath = parameters.RS_NodeExePath;
            string appiumJSPath = parameters.RS_AppiumJSPath;
            string serverIP = parameters.RS_ServerHost;
            bool autoDownloadChromeDriver = Convert.ToBoolean(parameters.RS_AutoDownloadChromeDriver ?? "false");
            AppiumServiceBuilder builder = new AppiumServiceBuilder();
            OptionCollector option = new OptionCollector();
            option.AddArguments(new KeyValuePair<string, string>(
                "--relaxed-security", string.Empty));
            option.AddArguments(new KeyValuePair<string, string>(
                "--allow-insecure", "adb_shell"));
            builder
                .UsingAnyFreePort()
                //.WithLogFile(new FileInfo(logFilePath))
                .UsingDriverExecutable(new FileInfo(nodeExePath))
                .WithAppiumJS(new FileInfo(appiumJSPath))
                .WithStartUpTimeOut(TimeSpan.FromSeconds(60))
                .WithIPAddress(serverIP)
                .WithArguments(option);

            if(autoDownloadChromeDriver) {
                KeyValuePair<string, string> argument = new KeyValuePair<string, string>("--allow-insecure", "chromedriver_autodownload");
                option.AddArguments(argument);
                builder.WithArguments(option);

            }

            AppiumLocalService service;
            try {
                service = builder.Build();
                service.Start();
                //MobileTestContext.Set(Constants.AppiumServerLogFileKey, logFilePath);
            } catch(Exception e) {
                throw new AppiumServiceException($"Cannot start appium server.Exception:\n{e}");
            }
            parameters.ServerUri = service.ServiceUrl;
            parameters.RS_ServerPort = service.ServiceUrl.Port.ToString();
            MobileTestContext.Set(Constants.AppiumServiceKey, service);

        }
        public static void Dispose() {
            MobileTestContext.Get<AppiumLocalService>(Constants.AppiumServiceKey,false)?.Dispose();
        }
    }
    
    
}