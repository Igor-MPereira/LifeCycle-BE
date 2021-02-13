using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Response
{
    public abstract class _BaseResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public _BaseResponse() { }
        public _BaseResponse(string message, bool success, HttpStatusCode statusCode)
        {
            Message = message;
            Success = success;
            StatusCode = statusCode;
        }
        public _BaseResponse(string message, bool success, int statusCode) : this(message, success, (HttpStatusCode)statusCode) { }
    }
}
