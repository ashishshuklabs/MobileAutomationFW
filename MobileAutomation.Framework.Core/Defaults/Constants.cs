namespace MobileAutomation.Framework.Core.Defaults {
    
    /// <summary>
    /// Device Types
    /// </summary>
    public enum DeviceType {
        ANDROIDPHONE,
        ANDROIDTABLET,
        IOSPHONE,
        IOSTABLET
    }

    public enum ApplicationType {
        NATIVE,
        HYBRID,
        WEB
    }

    public class Constants {
        public const string TestLogFolder = "TestLogs";
        public const string ScreenshotFolder = "Screenshots";
        public const string TestLogFileKey = "TestLogFile";
        public const string ScreenshotFileKey = "ScreenshotFile";
        public const string LogsKey = "Logs";
        public const string WaitTimeKey = "WaitTime";
        public const string AppiumAdditionalOptionsKey = "AppiumOptions";
        #region ServerDetails
        //Appium or grid hub Server already started 
        public const string RS_ServerResource = @"/wd/hub";
        public const string RS_ServerHost = @"127.0.0.1";

        public const string RS_ServerPort = "4444";
        #endregion
        public const string AppiumServerURI = "";
        public const string RS_BrowserName = "CHROME";
        public const string RS_AppType = "NATIVE";
        public const string RS_LocalExecution = "false";
        public const string RS_LocalExecutionAsService = "false";
        public const string RS_ImplicitWaitTime = "5";
        public const int DefaultWaitTime = 30;
        public const string RS_AppPackagePath = " ";
        public const string RS_AppPackage = "null";
        public const string RS_AppActivity = "null";
        public const string RS_AppPassword = "null";

        public const string RS_PlatformName = "Android";
        public const string RS_NewCommandTimeout = "60";
        public const string RS_PlatformVersion = "0";
        public const string RS_DeviceReadyTimeout = "120";
        public const string RS_DeviceName = "Android";
        public const string RS_NodeExePath = @"C:\Program Files\nodejs\node.exe";
        public const string RS_AppiumJSPath = @"C:\Users\ashish.shukla\AppData\Roaming\npm\node_modules\appium\build\lib\main.js";
        public const string RS_AutoDownloadChromeDriver = "false";
        public const string AppiumServerLogFileKey = "AppiumServerLogFile";
        public const string ServerUriKey = "ServerUri";
        public const string RS_DeviceGroup = "DeviceGroup";
        public const string AppiumServiceKey = "AppiumService";
        public const string TestEnvironmentKey = "TestEnvironmentParameters";
        public const string DevelopmentMode = "false";
        public const string ProxyKey = "Proxy";
    }
}