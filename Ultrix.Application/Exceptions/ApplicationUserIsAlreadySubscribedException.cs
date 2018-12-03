using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class ApplicationUserIsAlreadySubscribedException : Exception
    {
        public ApplicationUserIsAlreadySubscribedException()
        {
        }

        public ApplicationUserIsAlreadySubscribedException(string message) : base(message)
        {
        }

        public ApplicationUserIsAlreadySubscribedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplicationUserIsAlreadySubscribedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}