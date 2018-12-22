using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class SharedMemeAlreadyExistsException : Exception
    {
        public SharedMemeAlreadyExistsException()
        {
        }

        public SharedMemeAlreadyExistsException(string message) : base(message)
        {
        }

        public SharedMemeAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SharedMemeAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}