using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Tests.TestUtilities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;

namespace MobileAutomation.Framework.Core.Tests.IntegrationTests {
    [TestFixture]
    [NonParallelizable]
    [Category("Integration Tests")]
    public class ServerCommandExecutorTests : DriverSetup {
        public override void FixtureSetup() {
            DefaultTestRunParameters.Get();
            DefaultTestRunParameters.Set(nameof(Constants.RS_LocalExecution), "true");
            DefaultTestRunParameters.Set(nameof(Constants.RS_LocalExecutionAsService), "true");
            DefaultTestRunParameters.Set(nameof(Constants.RS_PlatformVersion), "8.1");
            base.FixtureSetup();
            this.TestEnvironmentParameters = new EnvironmentConfiguration().Get();
        }
        [Test]
        public void CanExecuteValidServerCommandSuccessfully() {
            ServerCommandExecutor executor = new ServerCommandExecutor();
            Response res = executor.Execute("getAvailableLogTypes");
            Assert.True(res.Status == OpenQA.Selenium.WebDriverResult.Success, "Response should have been successful for valid command.");
        }
        [Test]
        public void InvalidServerCommandThrows() {
            ServerCommandExecutor executor = new ServerCommandExecutor();
            Assert.Throws<NotSupportedException>(() => executor.Execute("testCommand"), "Response should have been un-successful for valid command.");
        }
        [Test]
        public void ServerLogsCanBeCapturedSuccessfully() {
            Assert.NotNull(TestLogs.WriteDeviceLogs().Find(f => f.Contains("server")), "Server logs should be generated");
        }
        
    }
}
