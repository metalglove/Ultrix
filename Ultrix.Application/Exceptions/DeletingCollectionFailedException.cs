using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class DeletingCollectionFailedException : Exception
    {
        public DeletingCollectionFailedException()
        {
        }

        public DeletingCollectionFailedException(string message) : base(message)
        {
        }

        public DeletingCollectionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletingCollectionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}