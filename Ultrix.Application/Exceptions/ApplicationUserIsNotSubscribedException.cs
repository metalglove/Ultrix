using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class ApplicationUserIsNotSubscribedException : Exception
    {
        public ApplicationUserIsNotSubscribedException()
        {
        }

        public ApplicationUserIsNotSubscribedException(string message) : base(message)
        {
        }

        public ApplicationUserIsNotSubscribedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplicationUserIsNotSubscribedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}