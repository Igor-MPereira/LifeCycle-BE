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
    using Controllers.Requests;
    using SocialMedia_LifeCycle.Domain.Other;
    using System.Net;
    using System.Security.Cryptography;
    using SocialMedia_LifeCycle.Domain.Response;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Configuration;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public class AuthService : IAuthService
    {
        private readonly LifeCycleContext _context;
        private readonly IConfiguration _config;
        private readonly string _BaseUserNotFoundMessage = "Usuário ou senha estão incorretos";

        public AuthService(LifeCycleContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IEnumerable<User> ToList()
        {
            return _context.Users.ToList();
        }

        public async Task<TokenResponse> RefreshAccessToken(RefreshTokenRequest refrToken)
        {
            var RToken = _context.RefreshTokenInfos.FirstOrDefault(r => r.RefreshToken.Equals(refrToken.RefreshToken) && r.UserName.Equals(r.UserName));

            if(RToken == null )
            {
                throw new ArgumentException("Refresh Token inexistente", nameof(refrToken), new UnauthorizedAccessException());
            }
              
            _context.RefreshTokenInfos.Remove(RToken);
            await _context.SaveChangesAsync();

            if(RToken.ValidTo < DateTime.UtcNow)
            {
                throw new LifeCycleException(HttpStatusCode.BadRequest, "O Refresh Token Expirou, Faça Login Novamente.");
            }

            return await GenerateAccessToken(RToken.UserName);
        }

        public async Task<TokenResponse> CreateNewAccount(UserCredentials userCredentials)
        {
            var ValidateUser = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals( userCredentials.Login) || u.Email.Equals(userCredentials.Email));

            if (ValidateUser != null && ValidateUser.Login == userCredentials.Login)
            {
                throw new LifeCycleException(HttpStatusCode.BadRequest, "Login inválido. Login já existe");
            } 
            else if (ValidateUser != null && ValidateUser.Email == userCredentials.Email)
            {
                throw new LifeCycleException(HttpStatusCode.BadRequest, "Email inválido, Email já existe");
            }

            var user = new User(userCredentials);

            var (hashedPassword, salt)  = NewPbkdf2EncryptedPassword(user.Password);
            user.Password = hashedPassword;
            user.Salt = salt;

            var userE = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Console.WriteLine(userE.State);
            return await GenerateAccessToken(user.Login);
        }

        private async Task<TokenResponse> GenerateAccessToken(string userLogin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var exp = DateTime.UtcNow.AddMinutes(10);
            
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("SecretKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(userLogin, exp),                
                Expires = exp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var refreshToken = GenerateRefreshToken();
            RefreshTokenInfo oldRefrToken = null;

            if((oldRefrToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(r => r.UserName == userLogin)) != null) 
            {
                _context.RefreshTokenInfos.Remove(oldRefrToken);
            }

            _context.RefreshTokenInfos.Add(new RefreshTokenInfo
            {
                RefreshToken = refreshToken,
                ValidTo = DateTime.UtcNow.AddDays(30),
                UserName = userLogin
            });
            await _context.SaveChangesAsync();

            return new TokenResponse(new TokenInfo { AccessToken = accessToken, RefreshToken = refreshToken, ValidTo = exp }, "Login efetuado com sucesso!");
        }

        public async Task<TokenResponse> Login(string login, string password, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => ( user.Login.Equals(login) || user.Email.Equals(email)));
            if ( user == null )
            {
                throw new ArgumentException(_BaseUserNotFoundMessage);
            }

            if ( !ValidatePbkdf2Password(password, user.Salt, user.Password) )
            {
                throw new ArgumentException(_BaseUserNotFoundMessage);
            }

            return await GenerateAccessToken(user.Login);
        }

        private ClaimsIdentity GetClaimsIdentity(string userLogin, DateTime? exp)
        {
            var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, userLogin)
                });

            return identity;
        }

        private (string, string) NewPbkdf2EncryptedPassword(string password)
        {
            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            var stringSalt = Convert.ToBase64String(salt);
            Console.WriteLine(stringSalt);
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                256 / 8));
            Console.WriteLine(hashedPassword);

            return (hashedPassword, stringSalt);
        }

        private bool ValidatePbkdf2Password(string inputPassword, string salt, string hashedPassword)
        {
            var decodedSubkey = Convert.FromBase64String(hashedPassword);
            var decodedSalt = Convert.FromBase64String(salt);

            var inputSubkey = KeyDerivation.Pbkdf2(
                inputPassword, 
                decodedSalt, 
                KeyDerivationPrf.HMACSHA256, 
                10000, 
                256 / 8);

            return CryptographicOperations.FixedTimeEquals(inputSubkey, decodedSubkey);
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            var refToken = Convert.ToBase64String(randomBytes);

            return refToken
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
        }        
    }
}
