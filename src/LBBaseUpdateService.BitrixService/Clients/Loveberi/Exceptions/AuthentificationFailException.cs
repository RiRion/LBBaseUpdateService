using System;
using System.Runtime.Serialization;

namespace BitrixService.Clients.Loveberi.Exceptions
{
    public class AuthentificationFailException : ApplicationException
    {
        public AuthentificationFailException()
        {
        }
        
        public AuthentificationFailException(string message) : base(message)
        {
        }

        public AuthentificationFailException(string message, Exception exception) : base(message, exception)
        {
        }

        protected AuthentificationFailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}