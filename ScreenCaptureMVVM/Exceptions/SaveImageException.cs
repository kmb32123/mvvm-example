using System.Runtime.Serialization;

namespace ScreenCaptureMVVM.Exceptions
{
    using System;

    [SerializableAttribute]
    public class ScreenCaptureException : Exception, ISerializable
    {
        public ScreenCaptureException()
        {
        }

        public ScreenCaptureException(string message)
            : base(message)
        {
        }

        public ScreenCaptureException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ScreenCaptureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}