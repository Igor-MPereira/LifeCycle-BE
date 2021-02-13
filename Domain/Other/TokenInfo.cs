using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Other
{
    public class TokenInfo
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
