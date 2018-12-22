using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class DeletingMemeLikeFailedException : Exception
    {
        public DeletingMemeLikeFailedException()
        {
        }

        public DeletingMemeLikeFailedException(string message) : base(message)
        {
        }

        public DeletingMemeLikeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletingMemeLikeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}