using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Shared.Services
{
    using Domain.Models;
    using Controllers.Requests;
    using SocialMedia_LifeCycle.Domain.Response;

    public interface IAuthService
    {
        IEnumerable<User> ToList();
        Task<TokenResponse> CreateNewAccount(UserCredentials userCredentials);
        Task<TokenResponse> RefreshAccessToken(RefreshTokenRequest refreshTokenInfo);
        Task<TokenResponse> Login(string login, string password, string email);

    }
}
