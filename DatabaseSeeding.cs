using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia_LifeCycle
{
    using DataAccessEF;
    using Domain.Models;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System.Security.Cryptography;

    public static class DatabaseSeeding
    {
        public static async Task Seed(LifeCycleContext context) 
        {
            if(!await context.Tags.AnyAsync())
            {
                var tags = new List<string>
                {
                    "Ciência",
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
                var user = new User()
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

                var salt = new byte[128 / 8];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
                var stringSalt = Convert.ToBase64String(salt);
                Console.WriteLine(stringSalt);
                var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    user.Password,
                    salt,
                    KeyDerivationPrf.HMACSHA256,
                    10000,
                    256 / 8
                    ));
                Console.WriteLine(hashedPassword);

                user.Password = hashedPassword;
                user.Salt = stringSalt;
                context.Users.Add(user);

                await context.SaveChangesAsync();
            }
        }
    }
}
