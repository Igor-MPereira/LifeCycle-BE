using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Response
{
    public class ApiResponse<TResponse> : _BaseResponse
    {
        public TResponse Value { get; set; }

        public ApiResponse() { }
        public ApiResponse(TResponse value, string message, bool success, int statusCode) : base(message, success, statusCode)
        {
            Value = value;
        }
        public ApiResponse(TResponse value, string message) : this(value, message, true, 200) { }
    }
}
