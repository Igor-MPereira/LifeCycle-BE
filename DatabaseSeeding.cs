using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia_LifeCycle
{
    using DataAccessEF;
    using Domain.Models;

    public static class DatabaseSeeding
    {
        public static async Task Seed(LifeCycleContext context) 
        {
            if(!await context.Tags.AnyAsync())
            {
                var tags = new List<string>
                {
                    "Ciência",
                    "Fofoca",
                    "Notícia",
                    "Conhecimentos Gerais",
                    "Celebridades",
                    "Cinema",
                    "Produtos",
                    "Vendas",
                    "Arte",
                    "Programação",
                    "HQs",
                    "Jogos",
                    "Séries",
                    "Animes",
                    "Fotos",
                    "Festa",
                    "NSFW",
                    "LGBTQIA+",
                    "Igualdade"
                };

                var seedTags = tags.Select(t => new Tag { IsDefault = true, Name = t });

                context.Tags.AddRange(seedTags);

                await context.SaveChangesAsync();
            }

            if (!await context.Users.AnyAsync())
            {
                var masterUser = new User()
                {
                    City = "Martinópolis",
                    State = "São Paulo",
                    Country = "Brasil",
                    PhoneNumber = "+55018997580058",
                    Email = "igor@gargantas.org",
                    Password = "IMP2718@LifeCycle",
                    Description = "LifeCycle Seed User",
                    DisplayName = "Igor Pereira, The Founder",
                    Explicit = true,
                    Login = "igor.pereira"                    
                };
                Console.WriteLine("helo");
                context.Users.Add(masterUser);

                await context.SaveChangesAsync();
            }
        }
    }
}
