using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class DeletingSharedMemeFailedException : Exception
    {
        public DeletingSharedMemeFailedException()
        {
        }

        public DeletingSharedMemeFailedException(string message) : base(message)
        {
        }

        public DeletingSharedMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletingSharedMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}