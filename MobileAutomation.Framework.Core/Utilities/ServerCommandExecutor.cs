using System;
using System.Collections.Generic;
using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;

namespace MobileAutomation.Framework.Core.Utilities {
    public enum StandardCommands {
        MoveTo,
        Flick,
        GetLogTypes,
        GetLog,
        SetOrientationLandscape,
        SetOrientationPortrait
    }
    /// <summary>
    /// Server command executor class. Utility for firing commands directly against the server.
    /// </summary>
    public class ServerCommandExecutor {
        public ServerCommandExecutor() : this(null, null) {
            //Check if the test context contains appropriate parameters, set them if true
            AppiumDriver<AppiumWebElement> driver = MobileTestContext.Get<AppiumDriver<AppiumWebElement>>(nameof(MobileDriver), false);
            TestEnvironmentParameters parameters = MobileTestContext.Get<TestEnvironmentParameters>(Constants.TestEnvironmentKey);
            Uri serverUri = parameters.ServerUri;
            this.sessionId = driver.SessionId;
            this.serverUri = serverUri;
        }
        public ServerCommandExecutor(Uri serverUri, SessionId sessionId, int commandExecutionTimeoutInSeconds = 10) {
            this.serverUri = serverUri;
            this.sessionId = sessionId;
            this.timeoutInSeconds = commandExecutionTimeoutInSeconds;

        }
        public Uri serverUri { get; set; }
        public SessionId sessionId { get; set; }
        public int timeoutInSeconds { get; set; }
        /// <summary>
        /// Execute Server Command
        /// </summary>
        /// <param name="commandToExecute">Command to execute</param>
        /// <param name="@params">parameters for the command</param>
        /// <param name="throwOnFailure">throw on command failure, default true</param>
        /// <typeparam name="T">type of the result expected</typeparam>
        /// <returns></returns>
        public Response Execute(string commandToExecute, Dictionary<string, object> @params = null, bool throwOnFailure = true) {
            Helper.IsNullOrEmpty(serverUri, $"{nameof(serverUri)} is empty");
            Helper.IsNullOrEmpty(sessionId, $"{nameof(sessionId)} is empty");
            if(string.IsNullOrEmpty(commandToExecute)) {
                throw new ArgumentNullException($"[{nameof(commandToExecute)}] cannot be null or empty.");
            }
            Response response = null;
            using(HttpCommandExecutor executor = new HttpCommandExecutor(serverUri, TimeSpan.FromSeconds(timeoutInSeconds))) {
                Command cmd = new Command(sessionId, commandToExecute, @params);
                if(executor.CommandInfoRepository.GetCommandInfo(commandToExecute) == null) {
                    throw new NotSupportedException($"Command [{commandToExecute}] is not supported with Appium/Selenium APIs.");
                }
                try {
                    response = executor.Execute(cmd);
                } catch(Exception e) {
                    if(throwOnFailure) {
                        throw new WebDriverException($"Execution of Command [{commandToExecute}] with parameters [{string.Join(',', @params.Values) ?? string.Empty}] failed!!.Exception:\n:{e}");
                    }
                }
            }

            if(response == null || response.Value == null) {
                return null;
            }
            return response;
        }
        public Response Execute(StandardCommands command, Dictionary<string, object> @params = null, bool throwOnFailure = true) {
            string commandToExecute = string.Empty;
            switch(command) {
                case StandardCommands.MoveTo:
                    commandToExecute = "touchMove";
                    return Execute(commandToExecute, @params, throwOnFailure);
                case StandardCommands.Flick:
                    commandToExecute = "flick";
                    return Execute(commandToExecute, @params, throwOnFailure);
                case StandardCommands.GetLogTypes:
                    return Execute("getAvailableLogTypes");
                case StandardCommands.GetLog:
                    return Execute("getLog", @params);
                case StandardCommands.SetOrientationLandscape:
                    return Execute("setOrientation", new Dictionary<string, object> { { "orientation", "LANDSCAPE" } });
                case StandardCommands.SetOrientationPortrait:
                    return Execute("setOrientation", new Dictionary<string, object> { { "orientation", "PORTRAIT" } });

            }
            return null;
        }

    }
}