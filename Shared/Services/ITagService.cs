using SocialMedia_LifeCycle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Shared.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> ToList();
    }
}
