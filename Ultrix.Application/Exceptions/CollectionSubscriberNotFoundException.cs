using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CollectionSubscriberNotFoundException : Exception
    {
        public CollectionSubscriberNotFoundException()
        {
        }

        public CollectionSubscriberNotFoundException(string message) : base(message)
        {
        }

        public CollectionSubscriberNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollectionSubscriberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}