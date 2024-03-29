﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.IdentityDTO
{
    public class AuthenticateResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool Is2StepVerificationRequired { get; set; }
        public string Provider { get; set; }
    }
}
