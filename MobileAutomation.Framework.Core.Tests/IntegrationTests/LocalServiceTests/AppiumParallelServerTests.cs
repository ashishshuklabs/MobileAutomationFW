using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using System;

namespace MobileAutomation.Framework.Core.Tests.IntegrationTests.LocalServiceTests {
    [Parallelizable(ParallelScope.All)]
    public class AppiumParallelServerTests {
        private string firstTestPortNumber;
        [Test]
        [Category("Integration Tests")]
        public void FirstAppiumServiceStartsIfLocalExecutionAndLocalExecutionAsServiceAreTrue() {
            TestEnvironmentParameters parameters = new TestEnvironmentParameters {
                RS_LocalExecution = "true",
                RS_LocalExecutionAsService = "true",
                RS_ServerHost = "127.0.0.1",
                RS_NodeExePath = Constants.RS_NodeExePath,
                RS_AppiumJSPath = Constants.RS_AppiumJSPath,
                RS_ServerPort = "0000"
            };
            AppiumServer.Start(parameters);
            Uri serviceUri = parameters.ServerUri;
            firstTestPortNumber = parameters.RS_ServerPort;
            Assert.True(MobileTestContext.ContainsKey(Constants.AppiumServiceKey), "Service should have started");
            Assert.NotNull(serviceUri, "Service should not be null.");
            Assert.AreEqual(serviceUri, $"http://{Constants.RS_ServerHost}:{parameters.RS_ServerPort}{Constants.RS_ServerResource}", "Service Url does not match default Url");
            Console.WriteLine(serviceUri.AbsoluteUri);
        }
        [Test]
        [Category("Integration Tests")]
        public void SecondAppiumServiceStartsIfLocalExecutionAndLocalExecutionAsServiceAreTrue() {
            TestEnvironmentParameters parameters = new TestEnvironmentParameters {
                RS_LocalExecution = "true",
                RS_LocalExecutionAsService = "true",
                RS_ServerHost = "127.0.0.1",
                RS_NodeExePath = Constants.RS_NodeExePath,
                RS_AppiumJSPath = Constants.RS_AppiumJSPath,
                RS_ServerPort = "0000"
            };
            AppiumServer.Start(parameters);
            Uri serviceUri = parameters.ServerUri;
            Assert.True(MobileTestContext.ContainsKey(Constants.AppiumServiceKey), "Service should have started");
            Assert.NotNull(serviceUri, "Service should not be null.");
            Assert.AreEqual(serviceUri, $"http://{Constants.RS_ServerHost}:{parameters.RS_ServerPort}{Constants.RS_ServerResource}", "Service Url does not match default Url");
            Assert.AreNotEqual(parameters.RS_ServerPort, firstTestPortNumber, "Service should start on different port numbers");
            Console.WriteLine(serviceUri.AbsoluteUri);
        }
        [TearDown]
        public void TestTest() {
            AppiumServer.Dispose();
        }
    }
}
