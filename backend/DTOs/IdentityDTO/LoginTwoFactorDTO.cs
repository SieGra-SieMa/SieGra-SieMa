using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.IdentityDTO
{
    public class LoginTwoFactorDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
