using MobileAutomation.Framework.Core.Defaults;
using MobileAutomation.Framework.Core.Entities;
using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace MobileAutomation.Framework.Core.Tests.UnitTests {
    [TestFixture]
    [Category("Unit Tests")]
    public class HelperTests {

        [Test]
        public void ProvidingValidBrowserNameReturnsCorrectEnumParameter() {
            Assert.AreEqual(MobileBrowserType.Chrome, Helper.SetBrowserType("chrome"), "Valid MobileBrowserType should have been returned.");
        }
        [Test]
        public void ProvidingInvalidBrowserNameThrows() {
            Assert.Throws<NotSupportedException>(() => Helper.SetBrowserType("Test"), "Invalid browser type should throw.");
        }
        [Test]
        public void EmptyBrowserNameShouldNotThrow() {
            Assert.DoesNotThrow(() => Helper.SetBrowserType(""), "Empty browser type should not throw.");
        }
        [Test]
        public void ProvidingValidApplicationTypeReturnsCorrectEnumParameter() {
            Assert.AreEqual(ApplicationType.NATIVE, Helper.SetApplicationType("native"), "Valid ApplicationType should have been returned.");
        }
        [Test]
        public void ProvidingInvalidApplicationTypeThrows() {
            Assert.Throws<NotSupportedException>(() => Helper.SetApplicationType("test"), "Invalid aplication type should throw.");
        }
        [Test]
        public void ProvidingValidServerHostPortAndResourceCreatesCorrectServerUri() {
            string host = "localhost";
            string port = "1234";
            string resource = "/wd/hub";
            Assert.AreEqual("http://localhost:1234/wd/hub", Helper.CreateUri(host, port, resource).AbsoluteUri, "Invalid Uri generated");
           

        }
        [Test]
        public void PassingEmptyStringInIsNullOrEmptyThrowsDefaultMessage() {
            string data = "";
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Helper.IsNullOrEmpty(data),"Passing empty string should throw.");
            Assert.That(ex.Message.Contains("Value cannot be null"), "Expected a default message in exception");
        }
        [Test]
        public void PassingEmptyStringInIsNullOrEmptyThrowsWithCustomMessage() {
            string data = "";
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => Helper.IsNullOrEmpty(data, $"Hello There:{nameof(data)}.!!"), "Passing empty string should throw.");
            Assert.That(ex.Message.Contains($"Hello There:{nameof(data)}.!!"), "Custom message should be printed");
        }
        [Test]
        public void PassingEmptyObjectInIsNullOrEmptyThrows() {
            AdditionalDriverOptions data = new AdditionalDriverOptions();
            Assert.DoesNotThrow(() => Helper.IsNullOrEmpty(data), "Passing empty object should throw.");
        }
        [Test]
        public void PassingNullObjectInIsNullDoesNotThrowAndReturnsFailedCheckTrueWhenFlagIsFalse() {
            AdditionalDriverOptions data = null;
            Assert.DoesNotThrow(() => Helper.IsNullOrEmpty(data,false), "Passing empty object should throw.");
            Assert.IsTrue(Helper.IsNullOrEmpty(data, false), "Check should have failed and returned true");
        }
        
    }
}
