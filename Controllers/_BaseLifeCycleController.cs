using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_LifeCycle.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _BaseLifeCycleController : ControllerBase
    {
        protected readonly string _BaseErrorMessage = "Algo inesperado deu errado ao tentar ";
        public IActionResult Error(string message, Exception exception, int statusCode = 500)
        {
            return StatusCode(statusCode, new ErrorResponse()
            {
                Details = exception.Message,
                Message = message,
                StatusCode = (HttpStatusCode)statusCode
            });
        }

        public ApiResponse<TResponse> GetResponse<TResponse>(TResponse result) where TResponse: class
        {
            if (result == null) return new ApiResponse<TResponse>
            {
                Message = "Ocorreu um erro!",
                Success = false
            };

            return new ApiResponse<TResponse>
            {
                Value = result,
                StatusCode = HttpStatusCode.OK,
                Success = true,
                Message = "A ação foi realizada com sucesso!"
            };
        }
    }
}
