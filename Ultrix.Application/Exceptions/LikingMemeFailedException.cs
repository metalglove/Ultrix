using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class LikingMemeFailedException : Exception
    {
        public LikingMemeFailedException()
        {
        }

        public LikingMemeFailedException(string message) : base(message)
        {
        }

        public LikingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LikingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
