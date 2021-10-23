using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.Services;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [AllowAnonymous]
        [HttpPost("create")]
        public ActionResult Create(AuthenticateRequestDTO accountRequestDTO)
        {
            try
            {
                var account = _accountService.Create(accountRequestDTO);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /*[AllowAnonymous]
        [HttpPost("authorize")]
        public IActionResult Authorize(AccountRequestDTO request)
        {
            try
            {
                var account = _accountService.Authorize(request, "abc");
                return Ok(account);
            }
            catch (Exception)
            {
                return Forbid();
            }
        }*/


        //Token methods:

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDTO requestDTO)
        {
            var response = _accountService.Authenticate(requestDTO, ipAddress());

            if (response == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _accountService.RefreshToken(refreshToken, ipAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);

        }


        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromBody] RevokeTokenDTO revokeTokenDTO)
        {
            // accept token from request body or cookie
            var token = revokeTokenDTO.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _accountService.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _accountService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _accountService.GetById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(int id)
        {
            var user = _accountService.GetById(id);
            if (user == null) return NotFound();

            return Ok(user.RefreshTokens);
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
