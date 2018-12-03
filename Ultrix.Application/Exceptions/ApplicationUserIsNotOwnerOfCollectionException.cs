using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class ApplicationUserIsNotOwnerOfCollectionException : Exception
    {
        public ApplicationUserIsNotOwnerOfCollectionException()
        {
        }

        public ApplicationUserIsNotOwnerOfCollectionException(string message) : base(message)
        {
        }

        public ApplicationUserIsNotOwnerOfCollectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplicationUserIsNotOwnerOfCollectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}