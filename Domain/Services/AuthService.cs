using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia_LifeCycle.Domain.Services
{
    using Shared.Services;
    using SocialMedia_LifeCycle.DataAccessEF;
    using Domain.Models;
    using Controllers.Params;
    using SocialMedia_LifeCycle.Domain.Other;
    using System.Net;
    using System.Security.Cryptography;

    public class AuthService : IAuthService
    {
        private readonly LifeCycleContext _context;

        public AuthService(LifeCycleContext context)
        {
            _context = context;
        }

        public IEnumerable<User> ToList()
        {
            return _context.Users.ToList();
        }

        public async Task<object> CreateNewAccount(UserCredentials userCredentials)
        {
            var ValidateUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == userCredentials.Login || u.Email == userCredentials.Email);

            if (ValidateUser != null && ValidateUser.Login == userCredentials.Login)
            {
                throw new LifeCycleException(HttpStatusCode.BadRequest, "Login inválido. Login já existe");
            } 
            else if (ValidateUser != null && ValidateUser.Email == userCredentials.Email)
            {
                throw new LifeCycleException(HttpStatusCode.BadRequest, "Email inválido, Email já existe");
            }

            var user = new User(userCredentials);

            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            var stringSalt = Convert.ToBase64String(salt);
            Console.WriteLine(stringSalt);
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                user.Password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256/8
                ));
            Console.WriteLine(hashedPassword);

            user.Password = hashedPassword;
            user.Salt = stringSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Task.FromResult(new object { });
        }
    }
}
