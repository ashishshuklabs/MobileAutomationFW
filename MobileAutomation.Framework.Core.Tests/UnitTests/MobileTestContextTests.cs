using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using System.Collections.Generic;

namespace MobileAutomation.Framework.Core.Tests.UnitTests {
    [TestFixture]
    [Category("Unit Tests")]
    public class MobileTestContextTests {
        [Test]
        public void RetreivingNonExistentContextValueThrowsByDefault() {
            Assert.Catch<KeyNotFoundException>(() => MobileTestContext.Get<string>("test"), "No key set in thread context must throw by default");
        }
        [Test]
        public void RetreivingNonExistentContextValueDoesNotThrowIfFlagSet() {
            Assert.DoesNotThrow(() => {
                MobileTestContext.Get<string>("test", false);
            }, "Fetching non existent value should not throw if throwOnNotFound flag is false");
        }
        [Test]
        public void ContextValueSetAndGetPasses() {
            Assert.DoesNotThrow(() => {
                MobileTestContext.Set("testKey", "testValue");
                MobileTestContext.Get<string>("testKey", false);
            }, "Retrieving set value should not throw.");
        }
    }
}
