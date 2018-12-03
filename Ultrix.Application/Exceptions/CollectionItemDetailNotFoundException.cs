using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CollectionItemDetailNotFoundException : Exception
    {
        public CollectionItemDetailNotFoundException()
        {
        }

        public CollectionItemDetailNotFoundException(string message) : base(message)
        {
        }

        public CollectionItemDetailNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollectionItemDetailNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}