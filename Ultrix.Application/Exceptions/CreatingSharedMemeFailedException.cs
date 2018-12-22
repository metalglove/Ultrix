using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CreatingSharedMemeFailedException : Exception
    {
        public CreatingSharedMemeFailedException()
        {
        }

        public CreatingSharedMemeFailedException(string message) : base(message)
        {
        }

        public CreatingSharedMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingSharedMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}