using System;
using System.Net;

namespace BitrixService.Clients.TypedHttp.Exceptions
{
    public class HttpServerException : ApplicationException
    {
        public HttpServerException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}