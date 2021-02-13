using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Other
{
    public class LifeCycleException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public LifeCycleException(HttpStatusCode statusCode) : base()
        {
            StatusCode = statusCode;
        }

        public LifeCycleException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public LifeCycleException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException) 
        {
            StatusCode = statusCode;
        }
    }
}
