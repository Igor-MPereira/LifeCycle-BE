using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Services
{
    using Shared.Services;
    using DataAccessEF;

    public class InteractionService : IInteractionService
    {
        private readonly LifeCycleContext _context;

        public InteractionService(LifeCycleContext context)
        {
            _context = context;
        }
    }
}
