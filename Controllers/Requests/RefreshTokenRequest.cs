using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Controllers.Requests
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
