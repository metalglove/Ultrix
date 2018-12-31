using System;
using System.Runtime.Serialization;

namespace Ultrix.Domain.Exceptions
{
    [Serializable]
    public class UpdatingEntityFailedException : Exception
    {
        public UpdatingEntityFailedException()
        {
        }

        public UpdatingEntityFailedException(string message) : base(message)
        {
        }

        public UpdatingEntityFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpdatingEntityFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}