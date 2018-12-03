using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    public class SavingMemeFailedException : Exception
    {
        public SavingMemeFailedException()
        {
        }

        public SavingMemeFailedException(string message) : base(message)
        {
        }

        public SavingMemeFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SavingMemeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
