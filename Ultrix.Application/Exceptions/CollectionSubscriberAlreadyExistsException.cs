using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CollectionSubscriberAlreadyExistsException : Exception
    {
        public CollectionSubscriberAlreadyExistsException()
        {
        }

        public CollectionSubscriberAlreadyExistsException(string message) : base(message)
        {
        }

        public CollectionSubscriberAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollectionSubscriberAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}