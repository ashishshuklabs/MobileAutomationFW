using MobileAutomation.Framework.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;

namespace MobileAutomation.Framework.Core.Tests.UnitTests {
    [TestFixture]
    [Category("Unit Tests")]
    public class CommandExecutorTests {
        [Test]
        public void NullCommandToExecuteParameterThrows() {
            Uri uri = Helper.CreateUri("localhost", "1234", @"/wd/hub");
            SessionId id = new SessionId("testKey");
            ServerCommandExecutor executor = new ServerCommandExecutor(uri, id);
            string commandToExecute = null;
            Assert.Throws<ArgumentNullException>(() => executor.Execute(commandToExecute), "null or empty command should throw.");
            
        }
        [Test]
        public void InvalidCommandThrows() {
            Uri uri = Helper.CreateUri("localhost", "1234", @"/wd/hub");
            SessionId id = new SessionId("testKey");
            ServerCommandExecutor executor = new ServerCommandExecutor(uri, id);
            string commandToExecute = "someTestCommand";
            Assert.Throws<NotSupportedException>(() => executor.Execute(commandToExecute), "unsupported command should throw");

        }
        [Test]
        public void NullUriThrows() {
            Uri uri = Helper.CreateUri("localhost", "1234", @"/wd/hub");
            SessionId id = new SessionId("testKey");
            ServerCommandExecutor executor = new ServerCommandExecutor(null, id);
            string commandToExecute = "getAvailableLogs";
            Assert.Throws<ArgumentNullException>(() => executor.Execute(commandToExecute), "null uri should throw");

        }
        [Test]
        public void NullSessionIdThrows() {
            Uri uri = Helper.CreateUri("localhost", "1234", @"/wd/hub");
            ServerCommandExecutor executor = new ServerCommandExecutor(uri, null);
            string commandToExecute = "someTestCommand";
            Assert.Throws<ArgumentNullException>(() => executor.Execute(commandToExecute), "null session id should throw");

        }
    }
}
