using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Shared.Services
{
    using Domain.Models;
    using Controllers.Params;

    public interface IAuthService
    {
        IEnumerable<User> ToList();
        Task<object> CreateNewAccount(UserCredentials userCredentials);
    }
}
