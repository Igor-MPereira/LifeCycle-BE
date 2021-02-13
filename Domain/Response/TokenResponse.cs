using SocialMedia_LifeCycle.Domain.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Response
{
    public class TokenResponse : ApiResponse<TokenInfo>
    {
        public TokenResponse(TokenInfo tokenInfo, string message) : base(tokenInfo, message) { }
    }
}
