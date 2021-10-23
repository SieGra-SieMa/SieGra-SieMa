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
using System.Collections.Generic;

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

        //register
        public User Create(AuthenticateRequestDTO authenticateRequestDTO)
        {
            var isExist = _context.Users
                .Where(e => e.Email == authenticateRequestDTO.Email)
                .SingleOrDefault();

            if (isExist != null)
            {
                throw new Exception("User already exists");
            }

            var salt = CreateSalt();

            var password = GetPassword(authenticateRequestDTO.Password, salt);

            var client = new User
            {
                Name = authenticateRequestDTO.Name,
                Surname = authenticateRequestDTO.Surname,
                Email = authenticateRequestDTO.Email,
                Password = password,
                Salt = salt
            };

            _context.Users.Add(client);
            _context.SaveChanges();

            client.Password = null;
            client.Salt = null;

            return client;
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

        //authorize
        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO request, string ipAddress)
        {
            //decode password with salt
            var salt = _context.Users
                .Where(e => e.Email == request.Email)
                .Select(e => e.Salt)
                .First();

            var password = GetPassword(request.Password, salt);

            //check if user provide correct password
            var user = _context.Users
                .Where(e => e.Email == request.Email
                && e.Password == password)
                .First();

            // return null if user not found
            if (user == null) return null;

            //generate jwt token
            var jwtToken = CreateAccessToken(user);

            //generate refresh token
            var refreshToken = CreateRefreshToken(ipAddress);

            //save generated refresh token to db
            user.RefreshTokens.Add(refreshToken);
            _context.Update(user);
            _context.SaveChanges();

            return new AuthenticateResponseDTO(user, jwtToken, refreshToken.Token);
        }

        //refresh token //szto tu się stanęło XD
        /*public AccountResponseDTO RefreshToken(string token, string ipAddress)
        {
            //check if user with this token already exists
            var user = _context.Users.Where(e => e.RefreshTokens.Any(t => t.Token == token)).First();

            //check if token is active
            var oldRefreshToken = user.RefreshTokens.Single(t => t.IsActive);

            if(!oldRefreshToken.IsActive)
            {
                throw new Exception("This token already expired");
            }

            //generate new jwt token
            var newJWTToken = CreateJwtToken(user);

            //generate new token and write it to user
            var newRefreshToken = CreateRefreshToken(ipAddress);

            //write old refresh token to db
            oldRefreshToken.Revoked = DateTime.UtcNow;
            oldRefreshToken.RevokedByIp = ipAddress;
            oldRefreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

            //return new tokens
            return new AccountResponseDTO(user, newJWTToken, newRefreshToken.Token);
        }*/
        public AuthenticateResponseDTO RefreshToken(string token, string ipAddress)
        {
            //check if user with this token already exists
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            //check if token is active
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = CreateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

            // generate new jwt
            var jwtToken = CreateAccessToken(user);

            return new AuthenticateResponseDTO(user, jwtToken, newRefreshToken.Token);
        }

        public bool RevokeToken(string token, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _context.Update(user);
            _context.SaveChanges();

            return true;
        }

        private string CreateAccessToken(User user)
        {
            //return "abc";
            var roles = user.UserRoles.ToList();
            var claims = new Claim[roles.Count + 1];
            claims[0] = new Claim(ClaimTypes.Name, user.Email);
            for(int i = 1; i <= roles.Count; i++)
            {
                claims[i] = new Claim(ClaimTypes.Role, roles[i-1].Role.Name);
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken
            (
                issuer: "SieGraSieMa",
                audience: "Users",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken CreateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }


        /*
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
        */



        /*
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
        }*/

        //Token methods:

    }
}
