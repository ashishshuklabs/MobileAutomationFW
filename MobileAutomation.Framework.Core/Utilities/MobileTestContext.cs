using System;
using System.Collections.Generic;
using MobileAutomation.Framework.Core.BaseSetup;
using MobileAutomation.Framework.Core.Defaults;

namespace MobileAutomation.Framework.Core.Utilities {
    public class MobileTestContext {
        [ThreadStatic] private static Dictionary<string, object> properties;

        private static Dictionary<string, object> Properties {
            get {
                if(properties == null) {
                    properties = new Dictionary<string, object>();
                }
                return properties;
            }
        }

        /// <summary>
        /// Set the context variable
        /// </summary>
        /// <typeparam name="T">type of value to save</typeparam>
        /// <param name="key">key for stored value</param>
        /// <param name="value">value to store</param>
        public static void Set<T>(string key, T value) {
            //Add thread details if unavailable

            if(string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException($"[{nameof(key)}] cannot be null or empty.");
            }

            if(!Properties.ContainsKey(key)) {
                Properties.Add(key, value);
                return;
            }
            //Already contains a key, remove and refresh it with a new one.
            Properties.Remove(key);
            Properties.Add(key, value);

        }
        /// <summary>
        /// Get a stored context value
        /// </summary>
        /// <typeparam name="T">type of the value to be retrieved</typeparam>
        /// <param name="key">key corresponding to the value</param>
        /// <returns></returns>
        public static T Get<T>(string key) {
            return Get<T>(key, true);
        }

        public static T Get<T>(string key, bool throwOnKeyNotFound) {
            if(string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException($"[{nameof(key)}] cannot be null or empty.");
            }
            if(!Properties.ContainsKey(key)) {
                if(throwOnKeyNotFound) {
                    throw new KeyNotFoundException($"Key: [{key}] not found in the context.");
                }

                return default(T);
            }
            return (T)Properties[key];
        }
        /// <summary>
        /// Helper method to check if a key exists in the test context
        /// </summary>
        /// <param name="key">key to search for in test context</param>
        /// <returns></returns>
        public static bool ContainsKey(string key) {
            if(string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException($"[{nameof(key)}] cannot be null or empty.");
            }
            if(Properties.ContainsKey(key)) {
                return true;
            }

            return false;
        }


        public static void Dispose() {
            Dispose(true);
        }

        protected static void Dispose(bool isNotDisposed) {
            if(isNotDisposed) {
                if(properties == null) {
                    return;
                }
                if(properties.ContainsKey(nameof(MobileDriver))) {
                    properties[nameof(MobileDriver)] = null;
                }
                if(properties.ContainsKey(Constants.AppiumServiceKey)) {
                    AppiumServer.Dispose();
                    properties[Constants.AppiumServiceKey] = null;
                }
                properties = null;
            }
        }
    }
}