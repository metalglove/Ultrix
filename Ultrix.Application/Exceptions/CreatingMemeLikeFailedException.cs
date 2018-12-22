using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CreatingMemeLikeFailedException : Exception
    {
        public CreatingMemeLikeFailedException()
        {
        }

        public CreatingMemeLikeFailedException(string message) : base(message)
        {
        }

        public CreatingMemeLikeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingMemeLikeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}