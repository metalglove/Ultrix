using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class DislikingMemeFailedException : Exception
    {
        public DislikingMemeFailedException()
        {
        }

        public DislikingMemeFailedException(string message) : base(message)
        {
        }

        public DislikingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DislikingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}