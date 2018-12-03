using System;
using System.Runtime.Serialization;

namespace Ultrix.Application.Exceptions
{
    [Serializable]
    internal class EntityValidationException : Exception
    {
        internal EntityValidationException()
        {
        }

        internal EntityValidationException(string message) : base(message)
        {
        }

        internal EntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal EntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}