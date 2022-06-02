using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.Users;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
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
        public async Task<ActionResult> ChangeUserDetails(UserDetailsDTO userDetailsDTO)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var newUser = _userService.UpdateUser(email, userDetailsDTO);
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(email));
                newUser.Roles = roles;
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("current")]
        public async Task<ActionResult> GetCurrentUserAsync()
        {
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var user = _userService.GetUser(email);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = roles });
            //new UserDTO { Id = user.Id, Name=user.Name, Surname=user.Surname, Email = user.NormalizedEmail}
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> GetCurrentUserAsync(UserPasswordDTO passwordDTO)
        {
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var user = _userService.GetUser(email);
            var response = await _userManager.ChangePasswordAsync(user, passwordDTO.OldPassword, passwordDTO.NewPassword);
            if (response.Succeeded)
                return Ok(new MessageDTO { Message = "Password sucsefully changed" });

            return BadRequest(response.Errors);
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

        [HttpDelete("admin/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "User not found" });
            await _userManager.DeleteAsync(user);
            return Ok();
        }

        [HttpGet("admin/all")]
        public async Task<ActionResult> GetUsers()
        {
            var users = _userManager.Users;
            var usersDTO = users.Select(u => new UserDTO { Id = u.Id, Name = u.Name, Surname = u.Surname, Email = u.NormalizedEmail});
            return Ok(usersDTO);
        }

        [HttpPost("admin/add-role/{id}")]
        public async Task<ActionResult> AddRoles(int id, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "User not found" });
            await _userManager.AddToRolesAsync(user, roles);
            
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = await _userManager.GetRolesAsync(user) });
        }

        [HttpPost("admin/remove-role/{id}")]
        public async Task<ActionResult> RemoveRole(int id, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "User not found" });
            await _userManager.RemoveFromRolesAsync(user, roles);

            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = await _userManager.GetRolesAsync(user) });
        }

        [HttpGet("newsletter/join")]
        public async Task<ActionResult> SubscribeToNewsletter()
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                _userService.JoinNewsletter(user.Id);
                return Ok(new MessageDTO { Message = "Newsletter joined"});
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }

        [HttpGet("newsletter/leave")]
        public async Task<ActionResult> UnsubscribeToNewsletter()
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                _userService.LeaveNewsletter(user.Id);
                return Ok(new MessageDTO { Message = "Newsletter unsubscribed" });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }
    }
}
