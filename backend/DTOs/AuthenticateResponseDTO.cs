using SieGraSieMa.Models;


namespace SieGraSieMa.DTOs
{
    public class AuthenticateResponseDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateResponseDTO(User User, string accessToken, string refreshToken)
        {
            this.Name = User.Name;
            this.Surname = User.Surname;
            this.Email = User.Email;
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }
    }
}
