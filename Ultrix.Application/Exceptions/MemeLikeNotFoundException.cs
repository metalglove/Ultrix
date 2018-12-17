using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class MemeLikeNotFoundException : Exception
    {
        public MemeLikeNotFoundException()
        {
        }

        public MemeLikeNotFoundException(string message) : base(message)
        {
        }

        public MemeLikeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemeLikeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}