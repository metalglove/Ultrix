using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class SharedMemeNotFoundException : Exception
    {
        public SharedMemeNotFoundException()
        {
        }

        public SharedMemeNotFoundException(string message) : base(message)
        {
        }

        public SharedMemeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SharedMemeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}