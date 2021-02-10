using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Services
{
    using DataAccessEF;
    using Shared.Services;

    public class CommentService : ICommentService
    {
        private readonly LifeCycleContext _context;

        public CommentService(LifeCycleContext context)
        {
            _context = context;
        }
    }
}
