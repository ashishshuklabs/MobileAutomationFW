using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;

namespace MobileAutomation.Framework.Core.Tests.TestUtilities {
    public class BaseTest {
        private Resources resources;
        protected TestEnvironmentParameters parameters;
        [OneTimeSetUp]
        public void Begin() {
            resources = new Resources();
            DefaultTestRunParameters.Get();
            parameters = new EnvironmentConfiguration().Get();

        }

        [SetUp]
        public void Start() {
            Resources.SetupFiles();
        }

        [OneTimeTearDown]
        public void Cleanup() {
            TestRunParameters.Dispose();
        }

    }
}