using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Services
{
    using Shared.Services;
    using SocialMedia_LifeCycle.DataAccessEF;

    public class AuthService : IAuthService
    {
        private readonly LifeCycleContext _context;

        public AuthService(LifeCycleContext context)
        {
            _context = context;
        }


    }
}
