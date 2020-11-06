using System;
using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace MobileAutomation.Framework.Core.BaseSetup {
    [TestFixture]
    public class DriverSetup {
        protected TestEnvironmentParameters TestEnvironmentParameters;
        protected DriverCapabilities Capabilities { get; private set; }
        /// <summary>
        /// Used to specify/override driver capabilities
        /// </summary>
        public AdditionalDriverOptions AdditionalCapabilities { get; protected set; }
        [OneTimeSetUp]
        public void OneTimeSetup() {
            FixtureSetup();

        }

        public virtual void FixtureSetup() {
            Resources.SetupFolders();
            TestEnvironmentParameters = new EnvironmentConfiguration().Get();

        }

        [SetUp]
        public void Setup() {
            AdditionalCapabilities = new AdditionalDriverOptions();
            //Setup Logs
            Resources.SetupFiles();//This goes as first line in TestSetup
            TestSetup();
        }

        public virtual void TestSetup() {
            
            //Start the service if needed
            AppiumServer.Start(TestEnvironmentParameters);
            //Set user provided options.
            MobileTestContext.Set(Constants.AppiumAdditionalOptionsKey, AdditionalCapabilities);
            Capabilities = new DriverCapabilities(TestEnvironmentParameters);
            //Merge user provided options to existing default capabilities.
            Capabilities.MergeCapabilities(MobileTestContext.Get<AdditionalDriverOptions>(Constants.AppiumAdditionalOptionsKey, false));
            // Write Runsettings to log file
            TestLogs.WriteLogSection("Original Test Run Parameters", () => TestLogs.Write(this.TestEnvironmentParameters.ToString()));
            MobileDriver.Initialize(Capabilities);
            //Write device configuration to log file
            TestLogs.WriteLogSection("DUT Configuration", () => TestLogs.Write(MobileDriver.Instance.Capabilities.ToString()));
            //Capture enviroment parameters for all futures uses.
            MobileTestContext.Set(Constants.TestEnvironmentKey, TestEnvironmentParameters);
            TestLogs.AddSection($"Test {TestContext.CurrentContext.Test.Name} Starts");
        }

        [TearDown]
        public void TearDown() {
            TestTearDown();
        }

        public virtual void TestTearDown() {

            try {
                if(TestContext.CurrentContext.Result.Outcome != ResultState.Success) {
                    Screenshot.Attach();
                }

                TestLogs.AddSection($"Test {TestContext.CurrentContext.Test.Name} Ends");
                TestLogs.Attach();
            } catch(Exception e) {
                throw new Exception($"Teardown failed. Urgent attention required!!! Exception: \n {e}");
            } finally {
                MobileDriver.Dispose();
            }

        }

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            FixtureTearDown();
        }

        public virtual void FixtureTearDown() {
        }
        /// <summary>
        /// Get Test Environment parameters. When called in a test returns all the current runsettings and overridden runsettings parameters.
        /// </summary>
        /// <returns></returns>
        public TestEnvironmentParameters GetParameters() {
            return TestEnvironmentParameters;
        }
    }
}