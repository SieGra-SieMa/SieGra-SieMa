using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SieGraSieMa.DTOs;
using SieGraSieMa.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SieGraSieMa.Services.Interfaces;

namespace SieGraSieMa.Services
{
    public class AccountService : IAccountService
    {
        private readonly SieGraSieMaContext _context;

        public IConfiguration Configuration { get; set; }

        public AccountService(SieGraSieMaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public User Create(AccountRequestDTO accountRequestDTO)
        {
            var isExist = _context.Users
                .Where(e => e.Email == accountRequestDTO.Email)
                .SingleOrDefault();

            if (isExist != null)
            {
                throw new Exception("User already exists");
            }

            var salt = CreateSalt();

            var password = GetPassword(accountRequestDTO.Password, salt);

            var client = new User
            {
                Name = accountRequestDTO.Name,
                Surname = accountRequestDTO.Surname,
                Email = accountRequestDTO.Email,
                Password = password,
                Salt = salt
            };

            _context.Users.Add(client);
            _context.SaveChanges();

            client.Password = null;
            client.Salt = null;

            return client;
        }

        public AccountResponseDTO Authorize(CredentialsDTO credentialsDTO)
        {

            var salt = _context.Users
                .Where(e => e.Email == credentialsDTO.Email)
                .Select(e => e.Salt)
                .First();

            var password = GetPassword(credentialsDTO.Password, salt);

            var user = _context.Users
                .Where(e => e.Email == credentialsDTO.Email
                && e.Password == password)
                .First();

            var token = CreateJwtToken(user);

            return new AccountResponseDTO() {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Token = token
            };
        }

        private string GetPassword(string password, string salt)
        {
            var valueBytes =
                       KeyDerivation.Pbkdf2(
                            password,
                            Encoding.UTF8.GetBytes(salt),
                            KeyDerivationPrf.HMACSHA512,
                            1000,
                            256 / 8
                        );
            return Convert.ToBase64String(valueBytes);
        }

        private string CreateSalt(int maximumSaltLength = 32)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        private string CreateJwtToken(User user)
        {
            var claims = new[]
               {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "Users")
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: "SieGraSieMa",
                audience: "Users",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );
            var refreshToken = Guid.NewGuid();
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
