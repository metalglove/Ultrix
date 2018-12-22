using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CreatingCollectionFailedException : Exception
    {
        public CreatingCollectionFailedException()
        {
        }

        public CreatingCollectionFailedException(string message) : base(message)
        {
        }

        public CreatingCollectionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingCollectionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}