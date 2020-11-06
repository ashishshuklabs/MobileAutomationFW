using System;
using System.Runtime.Serialization;

namespace MobileAutomation.Framework.Core.Utilities {
    [Serializable]
    public class MethodExecutionFailedException : Exception {
        public MethodExecutionFailedException() { }
        public MethodExecutionFailedException(string message) : base(message) { }
        public MethodExecutionFailedException(string message, Exception inner) : base(message, inner) { }
        protected MethodExecutionFailedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class ElementNotFoundException : Exception {
        public ElementNotFoundException() { }
        public ElementNotFoundException(string message) : base(message) { }
        public ElementNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ElementNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class AppiumServiceException : Exception {
        public AppiumServiceException() { }
        public AppiumServiceException(string message) : base(message) { }
        public AppiumServiceException(string message, Exception inner) : base(message, inner) { }
        protected AppiumServiceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class InvalidCapabilityException : Exception {
        public InvalidCapabilityException() {
        }

        public InvalidCapabilityException(string message) : base(message) {
        }

        public InvalidCapabilityException(string message, Exception innerException) : base(message, innerException) {
        }

        protected InvalidCapabilityException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
