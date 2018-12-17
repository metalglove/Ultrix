using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class UnLikingMemeFailedException : Exception
    {
        public UnLikingMemeFailedException()
        {
        }

        public UnLikingMemeFailedException(string message) : base(message)
        {
        }

        public UnLikingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnLikingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}