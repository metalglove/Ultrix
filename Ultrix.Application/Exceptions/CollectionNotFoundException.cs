using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CollectionNotFoundException : Exception
    {
        public CollectionNotFoundException()
        {
        }

        public CollectionNotFoundException(string message) : base(message)
        {
        }

        public CollectionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollectionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}