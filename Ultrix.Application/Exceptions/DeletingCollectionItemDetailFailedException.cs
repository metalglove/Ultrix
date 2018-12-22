using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class DeletingCollectionItemDetailFailedException : Exception
    {
        public DeletingCollectionItemDetailFailedException()
        {
        }

        public DeletingCollectionItemDetailFailedException(string message) : base(message)
        {
        }

        public DeletingCollectionItemDetailFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeletingCollectionItemDetailFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}