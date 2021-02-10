using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Enums
{
    public enum ELikeState: byte
    {
        None,
        Disliked,
        Liked,
        Favorited
    }
}
