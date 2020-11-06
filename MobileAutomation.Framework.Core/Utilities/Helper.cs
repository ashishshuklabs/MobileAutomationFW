using MobileAutomation.Framework.Core.Defaults;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace MobileAutomation.Framework.Core.Utilities {
    public class Helper {
        /// <summary>
        /// Convert string based browser name to MobileBrowserType enum
        /// </summary>
        /// <param name="browserName">browser name in string</param>
        /// <returns></returns>
        public static string SetBrowserType(string browserName) {
            switch(browserName.ToUpper()) {
                case "CHROME":
                    return MobileBrowserType.Chrome;
                case "SAFARI":
                    return MobileBrowserType.Safari;
                case "":
                    return string.Empty;
                default:
                     throw new NotSupportedException($"Browser type [{browserName}] not supported. Supported types [{string.Join(',', MobileBrowserType.Chrome, MobileBrowserType.Safari)}] or no-browser at all for non-web apps.");
                    
            }
        }
        /// <summary>
        /// Convert string based application type to ApplicationType enum
        /// </summary>
        /// <param name="applicationType">application type as a string</param>
        /// <returns></returns>
        public static ApplicationType SetApplicationType(string applicationType) {
            switch(applicationType.ToUpper()) {
                case "NATIVE":
                    return ApplicationType.NATIVE;
                case "HYBRID":
                    return ApplicationType.HYBRID;
                case "WEB":
                    return ApplicationType.WEB;

                default:
                    throw new NotSupportedException($"Application type [{applicationType}] is not supported! Allowed types: [{string.Join(',', ApplicationType.HYBRID, ApplicationType.NATIVE, ApplicationType.WEB)}]");
            }
        }
        /// <summary>
        /// Utility to create URI using provided hostName, port and resource
        /// </summary>
        /// <param name="hostName">Hostname</param>
        /// <param name="port">port</param>
        /// <param name="resource">resource</param>
        /// <returns></returns>
        public static Uri CreateUri(string hostName, string port, string resource) {
            return new Uri(@"http://" + hostName + ":" +
                        port + resource);
        }
        public static DeviceType SetDeviceType(string deviceType) {
            switch(deviceType) {
                case "ANDROIDPHONE":
                    return DeviceType.ANDROIDPHONE;
                case "IOSPHONE":
                    return DeviceType.IOSPHONE;
                case "ANDROIDTABLET":
                    return DeviceType.ANDROIDTABLET;
                case "IOSTABLET":
                    return DeviceType.IOSTABLET;
                default:
                    throw new NotSupportedException($"Device type [{deviceType}] is not supported.Supported types include [{string.Join(',', DeviceType.ANDROIDPHONE, DeviceType.ANDROIDTABLET, DeviceType.IOSPHONE, DeviceType.IOSTABLET)}]");
            }
        }
        /// <summary>
        /// Check if the specific instance is null or empty. Throws on exception
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="type">instance whos value is to be validated</param>
        /// <param name="customMessageInException">Optional custom message to be be added to default exception.</param>
        public static void IsNullOrEmpty<T>(T type, string customMessageInException = "") {
            IsNullOrEmpty(type, true, customMessageInException);
        }
        /// <summary>
        /// Check if the specific instance is null or empty. Throws on exception
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="type">instance whos value is to be validated</param>
        /// <param name="throwOnFailure">flag to indicate if exception to be thrown on failure, default true.</param>
        /// <param name="customMessageInException">optional custom exception to be added to exception</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(T type, bool throwOnFailure, string customMessageInException = "") {
            bool isFailCheck = false;
            if(type == null) {
                isFailCheck = true;
                if(!throwOnFailure) {
                    return isFailCheck;
                }
                throw new ArgumentNullException($"\n{customMessageInException}\nArgument [{typeof(T).Name}] is null.");
            }

            if(typeof(T).Name.Equals("string", StringComparison.OrdinalIgnoreCase)) {
                if(!string.IsNullOrEmpty(type.ToString())) {
                    return isFailCheck;
                }
                isFailCheck = true;
                if(!throwOnFailure) {
                    return isFailCheck;
                }
                throw new ArgumentNullException($"\n{customMessageInException}\nArgument [{typeof(T).Name}] is Empty.");
            }
            return isFailCheck;
        }


    }
    
}