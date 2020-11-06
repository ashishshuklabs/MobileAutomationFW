using System;
using MobileAutomation.Framework.Core.Defaults;
using Newtonsoft.Json;

namespace MobileAutomation.Framework.Core.Entities {
    public class TestEnvironmentParameters {
        public string RS_DeviceGroup { get; set; }
        public Uri ServerUri { get; set; }
        public ApplicationType RS_AppType { get; set; }
        public string RS_BrowserName { get; set; }
        public string RS_ImplicitWaitTime { get; set; }
        public string RS_AppPackagePath { get; set; }
        public string RS_AppPackage { get; set; }
        public string RS_AppActivity { get; set; }
        public string RS_AppPassword { get; set; }

        public string RS_ServerHost { get; set; }
        public string RS_ServerPort { get; set; }
        public string RS_PlatformName { get; set; }
        public string RS_NewCommandTimeout { get; set; }
        public string RS_PlatformVersion { get; set; }
        public string RS_DeviceReadyTimeout { get; set; }
        public string RS_DeviceName { get; set; }
        public string RS_NodeExePath { get; set; }
        public string RS_AppiumJSPath { get; set; }
        public string RS_AutoDownloadChromeDriver { get; set; }
        public string RS_LocalExecutionAsService { get; set; }
        public string RS_LocalExecution { get; set; }
        public override string ToString() {
            return JsonConvert.SerializeObject(this).Replace(",",",\n  ").Replace("{","{\n  ").Replace("}","\n}");
        }
    }
}