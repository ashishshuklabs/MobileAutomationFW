using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using System;
using System.IO;

namespace MobileAutomation.Framework.Core.Tests.IntegrationTests.LocalServiceTests {
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
   
    public class AppiumServerTests {
        [TearDown]
        public void TearDown() {
            if(MobileTestContext.ContainsKey(Constants.AppiumServerLogFileKey)) {
                if(File.Exists(Constants.AppiumServerLogFileKey)) {
                    File.Delete(Constants.AppiumServerLogFileKey);
                }
            }
            MobileTestContext.Dispose();
        }
        [Test]
        [Category("Unit Tests")]
        public void AppiumServiceDoesNotStartIfLocalExecutionAsServiceIsFalse() {
            TestEnvironmentParameters parameters = new TestEnvironmentParameters {
                RS_LocalExecution = "true",
                RS_LocalExecutionAsService = "false"
            };
            AppiumServer.Start(parameters);
            Assert.False(MobileTestContext.ContainsKey(Constants.AppiumServiceKey), "Service should not have started");

        }
        [Test]
        [Category("Unit Tests")]
        public void AppiumServiceShouldNotStartIfLocalExecutionIsFalse() {
            TestEnvironmentParameters parameters = new TestEnvironmentParameters {
                RS_LocalExecution = "false",
                RS_LocalExecutionAsService = "true"
            };
            AppiumServer.Start(parameters);
            Assert.False(MobileTestContext.ContainsKey(Constants.AppiumServiceKey), "Service should not have started");

        }
        [Test]
        [Category("Unit Tests")]
        public void NodeExePathEmptyThrows() {
            TestEnvironmentParameters parameters = new TestEnvironmentParameters {
                RS_LocalExecution = "true",
                RS_LocalExecutionAsService = "true",
                RS_ServerHost = "127.0.0.1",
                RS_NodeExePath = "",
                RS_AppiumJSPath = Constants.RS_AppiumJSPath
            };
            Assert.Throws<ArgumentNullException>(() => AppiumServer.Start(parameters), "Empty NodeExe path should throw.");
        }
        [Test]
        [Category("Unit Tests")]
        public void AppiumJSPathEmptyThrows() {
            TestEnvironmentParameters parameters = new TestEnvironmentParameters {
                RS_LocalExecution = "true",
                RS_LocalExecutionAsService = "true",
                RS_ServerHost = "127.0.0.1",
                RS_NodeExePath = Constants.RS_NodeExePath,
                RS_AppiumJSPath = ""
            };
            Assert.Throws<ArgumentNullException>(() => AppiumServer.Start(parameters), "Empty AppiumJS path should throw.");
        }
        [Test]
        [Category("Unit Tests")]
        public void HostNameEmptyThrows() {
            TestEnvironmentParameters parameters = new TestEnvironmentParameters {
                RS_LocalExecution = "true",
                RS_LocalExecutionAsService = "true",
                RS_ServerHost = "",
                RS_NodeExePath = Constants.RS_NodeExePath,
                RS_AppiumJSPath = Constants.RS_AppiumJSPath
            };
            Assert.Throws<ArgumentNullException>(() => AppiumServer.Start(parameters), "Empty Hostname should throw.");
        }
    }
}