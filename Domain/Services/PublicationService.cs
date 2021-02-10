using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Services
{
    using Shared.Services;
    using DataAccessEF;

    public class PublicationService : IPublicationService
    {
        private readonly LifeCycleContext _context;

        public PublicationService(LifeCycleContext context)
        {
            _context = context;
        }
    }
}
