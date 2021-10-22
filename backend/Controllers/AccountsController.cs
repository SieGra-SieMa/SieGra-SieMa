using Microsoft.AspNetCore.Authorization;
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
        public ActionResult Create(AccountRequestDTO accountRequestDTO)
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

        [AllowAnonymous]
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
        }


        //Token methods:

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            //var response = _userService.RefreshToken(refreshToken, ipAddress());

            //if (response == null)
            //    return Unauthorized(new { message = "Invalid token" });

            //setTokenCookie(response.RefreshToken);

            //return Ok(response);
            return null;
        }


        [HttpPost("revoke-token")]
        public IActionResult RevokeToken()
        {
            /*// accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _userService.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });*/
            return null;
        }
       
        [HttpGet("{id}/refresh-tokens")]
        public IActionResult GetRefreshTokens(int id)
        {
            /* var user = _userService.GetById(id);
             if (user == null) return NotFound();

             return Ok(user.RefreshTokens);*/
            return null;
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            /*var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);*/
        }

        private string ipAddress()
        {
            /* if (Request.Headers.ContainsKey("X-Forwarded-For"))
                 return Request.Headers["X-Forwarded-For"];
             else
                 return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();*/
            return null;
        }
    }
}
