using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class DeletingCollectionSubscriberFailedException : Exception
    {
        public DeletingCollectionSubscriberFailedException()
        {
        }

        public DeletingCollectionSubscriberFailedException(string message) : base(message)
        {
        }

        public DeletingCollectionSubscriberFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletingCollectionSubscriberFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}