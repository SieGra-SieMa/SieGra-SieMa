using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.IdentityDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticateResponseDTO = SieGraSieMa.DTOs.IdentityDTO.AuthenticateResponseDTO;
using RevokeTokenDTO = SieGraSieMa.DTOs.IdentityDTO.RevokeTokenDTO;

namespace SieGraSieMa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountIdentityServices _accountService;
        private readonly JwtHandler _jwtHandler;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILogService _logService;
        public IConfiguration _configuration { get; set; }



        public AccountsController(UserManager<User> userManager, JwtHandler jwtHandler, IEmailService emailService, IAccountIdentityServices accountServices, ILogService logService, IConfiguration configuration)
        {
            _accountService = accountServices;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _emailService = emailService;
            _logService = logService;
            _configuration = configuration;
        }
        private async Task<IActionResult> GenerateOTPFor2StepVerification(User user)
        {
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
            {
                return Unauthorized(new ResponseErrorDTO { Error = "Wrong provider" });
            }
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            await _emailService.SendAsync(user.Email, "Logowanie dwuetapowe", token);

            return Ok(new AuthenticateResponseDTO { Is2StepVerificationRequired = true, Provider = "Email" });
        }
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return BadRequest(new ResponseErrorDTO { Error = "Błędy adres email lub hasło" });

            if (await _userManager.IsLockedOutAsync(user))
                return BadRequest(new ResponseErrorDTO { Error = "Konto jest zablokowane" });

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest(new ResponseErrorDTO { Error = "Email nie został potwierdzony" });

            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {
                await _userManager.AccessFailedAsync(user);
                await _logService.AddLog(new Log(user, $"Account is locked out due to too much bad requests"));
                if (await _userManager.IsLockedOutAsync(user))
                    return BadRequest(new ResponseErrorDTO { Error = "Konto zostało zablokowane, ze wzgledu na zbyt dużą ilość błędnych prób logowania. Proszę spróbować za 10 minut" });

                return BadRequest(new ResponseErrorDTO { Error = "Błędy adres email lub hasło" });
            }

            if (await _userManager.GetTwoFactorEnabledAsync(user))
                return await GenerateOTPFor2StepVerification(user);

            var token = await _jwtHandler.GenerateToken(user);
            var refreshToken = await _accountService.CreateRefreshToken(user);
            SetRefreshTokenInCookie(refreshToken.Token);

            await _userManager.ResetAccessFailedCountAsync(user);

            return Ok(new AuthenticateResponseDTO { AccessToken = token, RefreshToken = refreshToken.Token });
        }

        [AllowAnonymous]
        [HttpPost("Verify")]
        public async Task<IActionResult> TwoStepVerification([FromBody] LoginTwoFactorDTO twoFactorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            var user = await _userManager.FindByEmailAsync(twoFactorDto.Email);
            if (user == null)
                return BadRequest(new ResponseErrorDTO { Error = "Wrong email" });

            var validVerification = await _userManager.VerifyTwoFactorTokenAsync(user, twoFactorDto.Provider, twoFactorDto.Token);
            if (!validVerification)
                return BadRequest(new ResponseErrorDTO { Error = "Wrong token" });

            var token = await _jwtHandler.GenerateToken(user);
            var refreshingToken = await _accountService.CreateRefreshToken(user);
            SetRefreshTokenInCookie(refreshingToken.Token);
            return Ok(new AuthenticateResponseDTO { AccessToken = token, RefreshToken = refreshingToken.Token });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerRequest)
        {
            if (registerRequest == null || !ModelState.IsValid)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            var user = new User
            {
                Name = registerRequest.Name,
                Surname = registerRequest.Surname,
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
                NormalizedEmail = registerRequest.Email.ToUpper(),
                NormalizedUserName = registerRequest.Email.ToUpper(),
                EmailConfirmed = false,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(" ", result.Errors.Select(e => e.Description));


                return BadRequest(new ResponseErrorDTO { Error = errors });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string>
            {
               {"userid", Convert.ToString(user.Id) },
               {"token", token }
            };

            //Email
            var link = $"{Request.Scheme}://{Request.Host}/api/Accounts/Confirm-Email";
            var callback = QueryHelpers.AddQueryString(link, param);

            await _emailService.SendAsync(user.Email, "Potwierdź konto email", callback);

            await _userManager.AddToRoleAsync(user, "User");

            await _logService.AddLog(new Log(user, $"Register successfully"));

            return Ok(new MessageDTO { Message = "Link weryfikujący adres został wysłany na Twój adres email!" });

        }
        //refresh token
        [AllowAnonymous]
        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromBody] RevokeTokenDTO model)
        {
            var refreshToken = model.RefreshToken ?? Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshToken(refreshToken);

            if (!response.IsAuthenticated)
                return Unauthorized(new ResponseErrorDTO { Error = "Wrong token" });

            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);

            return Ok(response);
        }
        //revoke token
        [AllowAnonymous]
        [HttpPost("Revoke-Token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDTO model)
        {
            // accept token from request body or cookie
            var token = model.RefreshToken ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });
            var response = _accountService.RevokeToken(token);
            if (!await response)
                return NotFound(new ResponseErrorDTO { Error = "Token not found" });
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {

            var userFound = await _userManager.FindByIdAsync(userid);
            if (userFound == null)
                return BadRequest(new ResponseErrorDTO { Error = "Wrong user id" });
            var result = (await _userManager.ConfirmEmailAsync(userFound, token));
            if (!result.Succeeded)
            {
                return BadRequest(new ResponseErrorDTO { Error = "Email not confirmed" });
            }
            await _logService.AddLog(new Log(userFound, $"Email confirmed successfully"));
            return Ok(new MessageDTO { Message = "Email is confirmed!" });
        }

        [AllowAnonymous]
        [HttpGet("Forget-Password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return BadRequest(new ResponseErrorDTO { Error = "User with this email doesn't exists!" });
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var front = _configuration.GetConnectionString("front");
                var param = new Dictionary<string, string>
                {
                    {"userid", Convert.ToString(user.Id) },
                    {"token", token }
                };
                var link = $"{front}/email-confirmation?";
                var callback = QueryHelpers.AddQueryString(link, param);
                await _emailService.SendAsync(user.Email, "Siegra Siema - reset hasła", callback);
                await _logService.AddLog(new Log(user, $"Email with password reset sent!"));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(string userid, string token,[FromBody] string newPassword)
        {

            var userFound = await _userManager.FindByIdAsync(userid);
            if (userFound == null)
                return BadRequest(new ResponseErrorDTO { Error = "Wrong user id!" });
            var result = (await _userManager.ResetPasswordAsync(userFound, token, newPassword));
            if (!result.Succeeded)
            {
                return BadRequest(new ResponseErrorDTO { Error = "Bad password!" });
            }
            await _logService.AddLog(new Log(userFound, $"Password changed successfully!"));
            return Ok(new MessageDTO { Message = "Password was changed!" });
        }
    }
}