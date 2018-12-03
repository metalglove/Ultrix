using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    public class GettingMemeFailedException : Exception
    {
        public GettingMemeFailedException()
        {
        }

        public GettingMemeFailedException(string message) : base(message)
        {
        }

        public GettingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GettingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
