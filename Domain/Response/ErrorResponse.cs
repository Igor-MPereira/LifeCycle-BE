using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Response
{
    public class ErrorResponse : _BaseResponse
    {
        public string Details { get; set; }
    }
}
