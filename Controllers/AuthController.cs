using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Shared.Services;
    using Requests;
    using Microsoft.AspNetCore.Authorization;
    using SocialMedia_LifeCycle.Domain.Other;
    using Microsoft.AspNetCore.Http;

    [ApiController]
    public class AuthController : _BaseLifeCycleController
    {
        private readonly string CookieName = "LFwaRTCoSt";
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("NewAccount")]
        public async Task<IActionResult> CreateNewAccount([FromBody] UserCredentials userCredentials)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var tokenResponse = await authService.CreateNewAccount(userCredentials);
                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                if(ex is LifeCycleException lcEx)
                {
                    return Error(lcEx.Message, lcEx, 400);
                }

                return Error(_BaseErrorMessage + "criar sua conta!", ex);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCredentials lCred)
        {
            try
            {
                var tokenResponse = await authService.Login(lCred.Login, lCred.Password, lCred.Email);

                setTokenCookie(tokenResponse.Value.RefreshToken);

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                if(ex is ArgumentException argEx)
                {
                    return Error(argEx.Message, argEx, 400);
                }

                return Error(_BaseErrorMessage + "efetuar login!", ex);
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest rTReq)
        {
            try
            {
                var token = rTReq.RefreshToken ?? Request.Headers[CookieName];
                var tokenResponse = await authService.RefreshAccessToken(rTReq);

                setTokenCookie(tokenResponse.Value.RefreshToken);

                return Ok(tokenResponse);
            }
            catch ( Exception ex )
            {
                if(ex is ArgumentException argEx)
                {
                    //Investigate?
                    return Error(_BaseErrorMessage + "renovar o token!", argEx);
                } 

                if(ex is LifeCycleException lcEx)
                {
                    return Error(lcEx.Message, lcEx, 400);
                }

                return Error(_BaseErrorMessage + "renovar o token!", ex);
            }
        }

        private void setTokenCookie(string rt)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(30),
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append(CookieName, rt, cookieOptions);
        }

        //private ipaddress for further security
    }
}
