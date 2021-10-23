using System;
using System.Collections.Generic;
using SieGraSieMa.DTOs;
using SieGraSieMa.Models;

namespace SieGraSieMa.Services.Interfaces
{
    public interface IAccountService
    {
        User Create(AuthenticateRequestDTO authenticateRequestDTO);
        //AccountResponseDTO Authorize(CredentialsDTO credentialsDTO);
        AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO request, string ipAddress);
        AuthenticateResponseDTO RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
