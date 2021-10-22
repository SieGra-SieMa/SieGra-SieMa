using System;
using SieGraSieMa.DTOs;
using SieGraSieMa.Models;

namespace SieGraSieMa.Services.Interfaces
{
    public interface IAccountService
    {
        User Create(AccountRequestDTO accountRequestDTO);
        //AccountResponseDTO Authorize(CredentialsDTO credentialsDTO);
        AccountResponseDTO Authorize(AccountRequestDTO model, string ipAddress);
        AccountResponseDTO RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);
    }
}
