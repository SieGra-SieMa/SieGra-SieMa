using SieGraSieMa.DTOs.IdentityDTO;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface IAccountIdentityServices
    {
        public Task<RefreshToken> CreateRefreshToken(User user);

        public Task<RefreshTokenDTO> RefreshToken(string token);

        public Task<bool> RevokeToken(string token);
    }
}
