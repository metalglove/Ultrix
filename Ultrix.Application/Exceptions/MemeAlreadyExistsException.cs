using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class MemeAlreadyExistsException : Exception
    {
        public MemeAlreadyExistsException()
        {
        }

        public MemeAlreadyExistsException(string message) : base(message)
        {
        }

        public MemeAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemeAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}