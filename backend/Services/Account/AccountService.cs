﻿using Microsoft.EntityFrameworkCore;
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

        public Task<List<User>> GetAll();

        public Task<User> GetById(int id);

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
            var user = _context.Users.Include(i => i.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            var refreshTokenDTO = new RefreshTokenDTO();
            if (user == null)
            {
                refreshTokenDTO.IsAuthenticated = false;
                refreshTokenDTO.Message = $"Token nie pasuje do żadnego użytkownika";
                return refreshTokenDTO;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                refreshTokenDTO.IsAuthenticated = false;
                refreshTokenDTO.Message = $"Token nie aktywny";
                return refreshTokenDTO;
            }

            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            await _context.SaveChangesAsync();
            var newJWTToken = await _jwtHandler.GenerateToken(user);

            //fill the DTO
            refreshTokenDTO.Token = newJWTToken;
            refreshTokenDTO.RefreshToken = newRefreshToken.Token;
            refreshTokenDTO.IsAuthenticated = true;
            refreshTokenDTO.Email = user.Email;
            refreshTokenDTO.UserName = user.UserName;
            refreshTokenDTO.RefreshTokenExpiration = newRefreshToken.Expires;

            return refreshTokenDTO;
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
            var user = _context.Users.Include(i => i.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            // return false if no user found with token
            if (user == null) return false;
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            // return false if token is not active
            if (!refreshToken.IsActive) return false;
            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
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

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }

}
