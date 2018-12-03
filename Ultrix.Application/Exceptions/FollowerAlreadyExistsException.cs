using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class FollowerAlreadyExistsException : Exception
    {
        public FollowerAlreadyExistsException()
        {
        }

        public FollowerAlreadyExistsException(string message) : base(message)
        {
        }

        public FollowerAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FollowerAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}