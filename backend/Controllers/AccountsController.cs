using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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

        public AccountsController(UserManager<User> userManager, JwtHandler jwtHandler, IEmailService emailService, IAccountIdentityServices accountServices, ILogService logService)
        {
            _accountService = accountServices;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _emailService = emailService;
            _logService = logService;
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
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user == null)
                    return BadRequest(new ResponseErrorDTO { Error = "Incorrect email or password" });

                if (await _userManager.IsLockedOutAsync(user))
                    return BadRequest(new ResponseErrorDTO { Error = "Account is locked out" });

                if (!await _userManager.IsEmailConfirmedAsync(user))
                    return BadRequest(new ResponseErrorDTO { Error = "Email is not confirmed" });

                if (!await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    await _userManager.AccessFailedAsync(user);
                    await _logService.AddLog(new Log(user, "Account is locked out due to too much bad requests"));
                    if (await _userManager.IsLockedOutAsync(user))
                        return BadRequest(new ResponseErrorDTO { Error = "Account is locked out due to too much bad requests" });

                    return BadRequest(new ResponseErrorDTO { Error = "Incorrect email or password" });
                }

                var token = await _jwtHandler.GenerateToken(user);
                var refreshToken = await _accountService.CreateRefreshToken(user);
                SetRefreshTokenInCookie(refreshToken.Token);

                await _userManager.ResetAccessFailedCountAsync(user);

                return Ok(new AuthenticateResponseDTO { AccessToken = token, RefreshToken = refreshToken.Token });
            }
            catch (Exception e)
            {
                //await _logService.AddLog(new Log(user, "Error while changing password"));
                return BadRequest(new ResponseErrorDTO
                {
                    Error = e.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerRequest)
        {
            try
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

                await _logService.AddLog(new Log(user, "Register succesfully"));

                return Ok(new MessageDTO { Message = "A verification link has been sent to your email!" });
            }
            catch (Exception e)
            {
                //await _logService.AddLog(new Log(user, "Error while changing password"));
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromBody] RevokeTokenDTO model)
        {
            try
            {
                var refreshToken = model.RefreshToken ?? Request.Cookies["refreshToken"];
                var response = await _accountService.RefreshToken(refreshToken);

                if (!string.IsNullOrEmpty(response.RefreshToken))
                    SetRefreshTokenInCookie(response.RefreshToken);

                return Ok(response);
            }
            catch (Exception e)
            {
                //await _logService.AddLog(new Log(user, "Error while changing password"));
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("Revoke-Token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDTO model)
        {
            try
            {
                var token = model.RefreshToken ?? Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new ResponseErrorDTO { Error = "Bad request" });
                var response = await _accountService.RevokeToken(token);
                if (!response)
                    return NotFound(new ResponseErrorDTO { Error = "Token not found" });
                return Ok();
            }
            catch (Exception e)
            {
                //await _logService.AddLog(new Log(user, "Error while changing password"));
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("Confirm-Email")]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userid);
                if (user == null)
                    return BadRequest(new ResponseErrorDTO { Error = "Wrong user id" });
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                    return BadRequest(new ResponseErrorDTO { Error = "Email not confirmed" });

                await _logService.AddLog(new Log(user, "Email confirmed succesfully"));
                return Ok(new MessageDTO { Message = "Email confirmed!" });
            }
            catch (Exception e)
            {
                //await _logService.AddLog(new Log(user, "Error while changing password"));
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
    }
}