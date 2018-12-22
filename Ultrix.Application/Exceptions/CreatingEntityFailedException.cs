using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CreatingEntityFailedException : Exception
    {
        public CreatingEntityFailedException()
        {
        }

        public CreatingEntityFailedException(string message) : base(message)
        {
        }

        public CreatingEntityFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingEntityFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}