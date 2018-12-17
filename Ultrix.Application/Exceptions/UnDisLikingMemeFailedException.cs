using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class UnDislikingMemeFailedException : Exception
    {
        public UnDislikingMemeFailedException()
        {
        }

        public UnDislikingMemeFailedException(string message) : base(message)
        {
        }

        public UnDislikingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnDislikingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}