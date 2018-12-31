using System;
using System.Runtime.Serialization;

namespace Ultrix.Domain.Exceptions
{
    [Serializable]
    public class DeletingEntityFailedException : Exception
    {
        public DeletingEntityFailedException()
        {
        }

        public DeletingEntityFailedException(string message) : base(message)
        {
        }

        public DeletingEntityFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletingEntityFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}