using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia_LifeCycle
{
    using DataAccessEF;
    using Microsoft.EntityFrameworkCore;

    public static class DbInitializer
    {
        public async static Task Initialize(LifeCycleContext context)
        {
            await context.Database.MigrateAsync();

            context.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
            await DatabaseSeeding.Seed(context);
        }
    }
}
