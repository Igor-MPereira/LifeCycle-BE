using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Shared.Services;
    using Params;
    using Microsoft.AspNetCore.Authorization;

    public class UserController : _BaseLifeCycleController
   {
        private readonly IAuthService authService;

        public UserController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("NewAccount")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNewAccount([FromBody] UserCredentials userCredentials)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await authService.CreateNewAccount(userCredentials);
                return Ok(authService.ToList());
            }
            catch (Exception ex)
            {
                return Error("Algo deu errado!", ex);
            }
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            return Ok(authService.ToList());
        }

        public async Task<IActionResult> Login()
        {
            return Ok();
        }
   }
}
