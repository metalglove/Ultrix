using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    public class ApplicationUserIsNotAuthorizedException : Exception
    {
        public ApplicationUserIsNotAuthorizedException()
        {
        }

        public ApplicationUserIsNotAuthorizedException(string message) : base(message)
        {
        }

        public ApplicationUserIsNotAuthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplicationUserIsNotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}