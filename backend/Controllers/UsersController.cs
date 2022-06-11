using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.Newsletter;
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
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly ILogService _logService;

        public UsersController(IUserService userService, UserManager<User> userManager, ILogService logService, IEmailService emailService)
        {
            _userService = userService;
            _userManager = userManager;
            _logService = logService;
            _emailService = emailService;
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
                await _logService.AddLog(new Log(newUser.Id, "Changed user details"));
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
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = roles });
            //new UserDTO { Id = user.Id, Name=user.Name, Surname=user.Surname, Email = user.NormalizedEmail}
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(UserPasswordDTO passwordDTO)
        {
            User user = null;
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                user = await _userManager.FindByEmailAsync(email);
                var response = await _userManager.ChangePasswordAsync(user, passwordDTO.OldPassword, passwordDTO.NewPassword);
                if (response.Succeeded)
                {
                    await _logService.AddLog(new Log(user, "Password changed succesfully"));
                    return Ok(new MessageDTO { Message = "Password succesfully changed" });

                }
                await _logService.AddLog(new Log(user, "Password does not changed - password does not meet the requirements"));
                return BadRequest(new ResponseErrorDTO
                {
                    Error = string.Join(" ", response.Errors.Select(e => e.Description))
                });
            }
            catch (Exception e)
            {
                await _logService.AddLog(new Log(user, "Error while changing password"));
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
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
        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpGet("admin/all")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserDTO> usersDTO = new List<UserDTO>();
            foreach(var user in users)
            {
                usersDTO.Add(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = await _userManager.GetRolesAsync(user) });
            }
            
            return Ok(usersDTO);
        }

        [HttpPost("admin/add-role/{id}")]
        public async Task<ActionResult> AddRoles(int id, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "User not found" });
            await _userManager.AddToRolesAsync(user, roles);
            await _logService.AddLog(new Log(user, "Add roles " + roles.Aggregate((i, j) => i + ", " + j) + " to " + user.UserName));
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = await _userManager.GetRolesAsync(user) });
        }

        [HttpPost("admin/remove-role/{id}")]
        public async Task<ActionResult> RemoveRole(int id, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "User not found" });
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _logService.AddLog(new Log(user, "Remove roles " + roles.Aggregate((i, j) => i + ", " + j) + " from " + user.UserName));
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.NormalizedEmail, Roles = await _userManager.GetRolesAsync(user) });
        }

        [HttpPost("admin/newsletter/send")]
        public async Task<ActionResult> SendNewsletter(NewsletterInfoDTO newsletter)
        {
            try
            {
                var users = await _userService.GetNewsletterSubscribers(newsletter.TournamentId);
                foreach(var user in users)
                {
                    await _emailService.SendAsync(user.NormalizedEmail, newsletter.Title, newsletter.Body);
                }
                return Ok(new MessageDTO { Message = "Newsletter sent!" });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }

        [HttpGet("newsletter/join")]
        public async Task<ActionResult> SubscribeToNewsletter()
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                _userService.JoinNewsletter(user.Id);
                return Ok(new MessageDTO { Message = "Newsletter joined" });
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
                var user = await _userManager.FindByEmailAsync(email);
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
