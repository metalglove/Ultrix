using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class MemeNotFoundException : Exception
    {
        public MemeNotFoundException()
        {
        }

        public MemeNotFoundException(string message) : base(message)
        {
        }

        public MemeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}