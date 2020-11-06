using System;
using System.IO;
using MobileAutomation.Framework.Core.Defaults;
using NUnit.Framework;

namespace MobileAutomation.Framework.Core.Utilities {
    public class Resources {
        internal static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        private static string testLogFolder = Path.Combine(BasePath, Constants.TestLogFolder);
        private static string screenshotFolder = Path.Combine(testLogFolder, Constants.ScreenshotFolder);
        /// <summary>
        /// Setup test log and screenshot folders
        /// </summary>
        public static void SetupFolders() {
            if(!Directory.Exists(testLogFolder)) {
                Directory.CreateDirectory(testLogFolder);
            }

            if(!Directory.Exists(screenshotFolder)) {
                Directory.CreateDirectory(screenshotFolder);
            }
        }
        /// <summary>
        /// Set up test artifacts, remove old log files and screenshots.
        /// </summary>
        public static void SetupFiles() {
            SetupTestLog();
            CleanupScreenshots();
        }

        private static void SetupTestLog() {
            string testFileNameFullPath = Path.Combine(testLogFolder, TestContext.CurrentContext.Test.Name + ".log");
            if(File.Exists(testFileNameFullPath)) {
                try {
                    File.Delete(testFileNameFullPath);
                } catch(UnauthorizedAccessException e) {
                    Console.WriteLine("Cannot cleanup test log file", e);
                } catch(Exception ex) {
                    Console.WriteLine("Cannot cleanup test log file", ex);
                }
            }

            File.Create(testFileNameFullPath).Close();

            MobileTestContext.Set(Constants.TestLogFileKey, testFileNameFullPath);
        }

        private static void CleanupScreenshots() {
            string testScreenshotFullPath = Path.Combine(screenshotFolder, TestContext.CurrentContext.Test.Name + ".jpeg");
            if(File.Exists(testScreenshotFullPath)) {
                try {
                    File.Delete(testScreenshotFullPath);
                } catch(UnauthorizedAccessException e) {
                    Console.WriteLine("Cannot cleanup test screenshot file", e);
                } catch(Exception ex) {
                    Console.WriteLine("Cannot cleanup test screenshot file", ex);
                }
            }
            MobileTestContext.Set(Constants.ScreenshotFileKey, testScreenshotFullPath);
        }

    }
}