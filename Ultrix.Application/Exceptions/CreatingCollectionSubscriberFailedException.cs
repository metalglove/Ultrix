using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CreatingCollectionSubscriberFailedException : Exception
    {
        public CreatingCollectionSubscriberFailedException()
        {
        }

        public CreatingCollectionSubscriberFailedException(string message) : base(message)
        {
        }

        public CreatingCollectionSubscriberFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingCollectionSubscriberFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}