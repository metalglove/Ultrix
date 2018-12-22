using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class MemeLikeAlreadyExistsException : Exception
    {
        public MemeLikeAlreadyExistsException()
        {
        }

        public MemeLikeAlreadyExistsException(string message) : base(message)
        {
        }

        public MemeLikeAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemeLikeAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}