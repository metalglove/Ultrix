using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CreatingCollectionItemDetailFailedException : Exception
    {
        public CreatingCollectionItemDetailFailedException()
        {
        }

        public CreatingCollectionItemDetailFailedException(string message) : base(message)
        {
        }

        public CreatingCollectionItemDetailFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingCollectionItemDetailFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}