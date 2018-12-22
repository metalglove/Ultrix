using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class DeletingMemeFailedException : Exception
    {
        public DeletingMemeFailedException()
        {
        }

        public DeletingMemeFailedException(string message) : base(message)
        {
        }

        public DeletingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}