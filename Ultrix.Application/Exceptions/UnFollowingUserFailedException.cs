using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class UnFollowingUserFailedException : Exception
    {
        public UnFollowingUserFailedException()
        {
        }

        public UnFollowingUserFailedException(string message) : base(message)
        {
        }

        public UnFollowingUserFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnFollowingUserFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}