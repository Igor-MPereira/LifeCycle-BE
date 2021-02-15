using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Services
{
    using Shared.Services;
    using DataAccessEF;
    using SocialMedia_LifeCycle.Domain.Models;

    public class TagService : ITagService
    {
        private readonly LifeCycleContext _context;

        public TagService(LifeCycleContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> ToList()
        {
            return _context.Tags.ToList();
        }
    }
}
