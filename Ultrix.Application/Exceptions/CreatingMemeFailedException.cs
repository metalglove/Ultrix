using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CreatingMemeFailedException : Exception
    {
        public CreatingMemeFailedException()
        {
        }

        public CreatingMemeFailedException(string message) : base(message)
        {
        }

        public CreatingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
