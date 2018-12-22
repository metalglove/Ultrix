using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CollectionItemDetailAlreadyExistsException : Exception
    {
        public CollectionItemDetailAlreadyExistsException()
        {
        }

        public CollectionItemDetailAlreadyExistsException(string message) : base(message)
        {
        }

        public CollectionItemDetailAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollectionItemDetailAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}