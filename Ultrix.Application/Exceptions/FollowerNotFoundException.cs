using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class FollowerNotFoundException : Exception
    {
        public FollowerNotFoundException()
        {
        }

        public FollowerNotFoundException(string message) : base(message)
        {
        }

        public FollowerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FollowerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}