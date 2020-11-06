using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MobileAutomation.Framework.Core.Entities {
    public class AdditionalDriverOptions
    {
        public AdditionalDriverOptions() {
            this.additionalArguments = new List<string>();
            this.additionalCapabilities = new Dictionary<string, object>();
        }

        private Dictionary<string, object> additionalCapabilities;
        private List<string> additionalArguments;
        /// <summary>
        /// Add additional Capabilities
        /// </summary>
        /// <param name="key">capability key</param>
        /// <param name="value">value for capability</param>
        /// <returns></returns>
        public AdditionalDriverOptions AddCapability(string key, object value) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("Key cannot be empty/null");
            }
            if(this.additionalCapabilities.ContainsKey(key)) {
                TestContext.WriteLine($"Capability [{key}] already exists with value [{this.additionalCapabilities[key]}], overwriting with [{value}] ");
                this.additionalCapabilities.Remove(key);
            }
            this.additionalCapabilities.Add(key, value);
            return this;
        }


        /// <summary>
        /// Add additional arguments to driver 
        /// </summary>
        /// <param name="argument"> argument option to add</param>
        /// <returns></returns>
        public AdditionalDriverOptions AddArguments(string argument) {
            if (string.IsNullOrEmpty(argument)) {
                throw new ArgumentException("Argument cannot be null or empty");
            }
            this.additionalArguments.Add(argument);
            return this;
        }
        /// <summary>
        /// Get user defined arguments
        /// </summary>
        /// <returns></returns>
        public List<string> GetArguments() {
            return this.additionalArguments;
        }
        /// <summary>
        /// Get user added capabilities
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetCapabilities() {
            return this.additionalCapabilities;
        }
    }

}