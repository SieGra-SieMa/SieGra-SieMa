using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SieGraSieMa.DTOs.IdentityDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface IAccountIdentityServices
    {
        public Task<RefreshToken> CreateRefreshToken(User user);

        public Task<RefreshTokenDTO> RefreshToken(string token);

        public Task<bool> RevokeToken(string token);

    }
    public class AccountService : IAccountIdentityServices
    {
        private readonly SieGraSieMaContext _context;

        private readonly JwtHandler _jwtHandler;

        public IConfiguration Configuration { get; set; }

        public AccountService(SieGraSieMaContext context, IConfiguration configuration, JwtHandler jwtHandler)
        {
            _context = context;
            _jwtHandler = jwtHandler;
            Configuration = configuration;
        }

        public async Task<RefreshTokenDTO> RefreshToken(string token)
        {
            var user = await _context.Users.Include(i => i.RefreshTokens).Where(u => u.RefreshTokens.Any(t => t.Token == token)).SingleOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found!");

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new Exception("Token is not active!");

            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            await _context.SaveChangesAsync();
            var newJWTToken = await _jwtHandler.GenerateToken(user);

            return new RefreshTokenDTO {Token = newJWTToken, RefreshToken = newRefreshToken.Token };
        }

        public async Task<RefreshToken> CreateRefreshToken(User user)
        {
            RefreshToken token = null;
            if (user.RefreshTokens.Any(a => a.IsActive))
            {
                token = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
            }
            else
            {
                token = CreateRefreshToken();
            }
            user.RefreshTokens.Add(token);
            _context.Update(user);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<bool> RevokeToken(string token)
        {
            var user = await _context.Users.Include(i => i.RefreshTokens).Where(u => u.RefreshTokens.Any(t => t.Token == token)).SingleOrDefaultAsync();
            if (user == null)
                throw new Exception("User does not exists!");

            var refreshToken = user.RefreshTokens.First(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new Exception("Token is not active!");

            refreshToken.Revoked = DateTime.UtcNow;
            _context.Update(user);
            return await _context.SaveChangesAsync() > 1;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow,
                CreatedByIp = "123"
            };
        }
    }

}
