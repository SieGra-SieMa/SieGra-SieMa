using SieGraSieMa.Models;
using System;
namespace SieGraSieMa.DTOs
{
    public class AccountResponseDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AccountResponseDTO(User User, string AccessToken, string RefreshToken)
        {
            this.Name = User.Name;
            this.Surname = User.Surname;
            this.Email = User.Email;
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
        }
    }
}
