﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.Users;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly UserManager<User> _userManager;


        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPatch("change-details")]
        public ActionResult ChangeUserDetails(UserDetailsDTO userDetailsDTO)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claim = identity.Claims;
                var email = claim.Where(e => e.Type == ClaimTypes.Name).First().Value;
                _userService.UpdateUser(email, userDetailsDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("current")]
        public async Task<ActionResult> GetCurrentUserAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var email = claim.Where(e => e.Type == ClaimTypes.Name).First().Value;
            var user = _userService.GetUser(email);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = roles });
            //new UserDTO { Id = user.Id, Name=user.Name, Surname=user.Surname, Email = user.NormalizedEmail}
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "User not found" });
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = roles });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "User not found" });
            await _userManager.DeleteAsync(user);
            return Ok();
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users.Select(u => new UserDTO { Id = u.Id, Name = u.Name, Surname = u.Surname, Email = u.NormalizedEmail }));
        }
    }
}
