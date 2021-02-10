using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle.Domain.Enums
{
    public enum ERelationNature : byte
    {
        Blocked = 0,
        Forggoten = 1,
        Unwanted = 2,
        Friend = 3,
        Close = 4
    }
}
