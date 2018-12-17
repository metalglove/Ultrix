using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class FollowingUserFailedException : Exception
    {
        public FollowingUserFailedException()
        {
        }

        public FollowingUserFailedException(string message) : base(message)
        {
        }

        public FollowingUserFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FollowingUserFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}