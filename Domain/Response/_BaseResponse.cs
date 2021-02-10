using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Response
{
    public class _BaseResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
}
