using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class CollectionAlreadyExistsException : Exception
    {
        public CollectionAlreadyExistsException()
        {
        }

        public CollectionAlreadyExistsException(string message) : base(message)
        {
        }

        public CollectionAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CollectionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}