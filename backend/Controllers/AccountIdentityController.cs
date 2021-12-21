using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.IdentityDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using SieGraSieMa.Services.Email;
using SieGraSieMa.Services.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountIdentityController : ControllerBase
    {
        private readonly IAccountIdentityServices _accountService;

        private readonly JwtHandler _jwtHandler;

        private readonly UserManager<User> _userManager;

        //private readonly IMapper _mapper;

        private readonly IEmailService _emailService;

        public AccountIdentityController(UserManager<User> userManager, JwtHandler jwtHandler, IEmailService emailService, IAccountIdentityServices accountServices)
        {
            _accountService = accountServices; //IAccountServices accountServices
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            //_mapper = mapper; IMapper mapper, 
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized(new AuthenticateResponseDTO { ErrorMessage = "Email is not confirmed" });

            if (!await _userManager.CheckPasswordAsync(user, login.Password))
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                {
                    return Unauthorized(new AuthenticateResponseDTO { ErrorMessage = "The account is locked out" });
                }

                return Unauthorized(new AuthenticateResponseDTO { ErrorMessage = "Invalid Authentication" });
            }

            if (await _userManager.GetTwoFactorEnabledAsync(user))
                return await GenerateOTPFor2StepVerification(user);

            var token = await _jwtHandler.GenerateToken(user);
            var refreshToken = await _accountService.CreateRefreshToken(user);
            SetRefreshTokenInCookie(refreshToken.Token);

            await _userManager.ResetAccessFailedCountAsync(user);

            return Ok(new AuthenticateResponseDTO { IsAuthSuccessful = true, Token = token, RefreshingToken = refreshToken.Token });
           // return Ok();
        }

        private async Task<IActionResult> GenerateOTPFor2StepVerification(User user)
        {
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
            {
                return Unauthorized(new AuthenticateResponseDTO { ErrorMessage = "Invalid 2-Step Verification Provider." });
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            //https://ethereal.email/
            //_emailService.Send("jennyfer.erdman1@ethereal.email", user.Email, "Two Factor", token);

            return Ok(new AuthenticateResponseDTO { Is2StepVerificationRequired = true, Provider = "Email" }); //, Token = token 
        }

        [AllowAnonymous]
        [HttpPost("TwoStepVerification")]
        public async Task<IActionResult> TwoStepVerification([FromBody] LoginTwoFactorDTO twoFactorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(twoFactorDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            var validVerification = await _userManager.VerifyTwoFactorTokenAsync(user, twoFactorDto.Provider, twoFactorDto.Token);
            if (!validVerification)
                return BadRequest("Invalid Token Verification");

            var token = await _jwtHandler.GenerateToken(user);
            //var refreshingToken = await _accountService.CreateRefreshToken(user);
            //SetRefreshTokenInCookie(refreshingToken.Token);
            //return Ok(new AuthenticateResponseDTO { IsAuthSuccessful = true, Token = token, RefreshingToken = refreshingToken.Token });
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerRequest)
        {
            if (registerRequest == null || !ModelState.IsValid)
                return BadRequest();

            //var user = _mapper.Map<User>(registerRequest);

            var user = new User { };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                 var errors = result.Errors.Select(e => e.Description);
            
                 return BadRequest(new RegisterResponseDTO { Errors = errors });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string>
            {
               {"token", token },
                {"email", user.Email }
            };

            //Email
            //var callback = QueryHelpers.AddQueryString(userForRegistration.ClientURI, param);

            //var message = new Message(new string[] { "codemazetest@gmail.com" }, "Email Confirmation token", callback, null);
            //await _emailSender.SendEmailAsync(message);

            //TODO change role
            //await _userManager.AddToRoleAsync(user, "Viewer");

            return Ok(token);
            //return Ok();
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

        
        [HttpGet("getxd")]
        public async Task<IActionResult> GetXD()
        {
            return Ok("xd");
        }

        //refresh token
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshToken(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);

             return Ok(response);
        }

        //revoke token
        [AllowAnonymous]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDTO model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });
            var response = _accountService.RevokeToken(token);
            if (!await response)
                return NotFound(new { message = "Token not found" });
            return Ok(new { message = "Token revoked" });
        }
    }
}