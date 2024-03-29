﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.Newsletter;
using SieGraSieMa.DTOs.Pagging;
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

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPatch("change-details")]
        public async Task<ActionResult> ChangeUserDetails(UserDetailsDTO userDetailsDTO)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.UpdateUser(email, userDetailsDTO);
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(email));
                user.Roles = roles;
                await _logService.AddLog(new Log(user.Id, $"Changed user details"));
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpGet("current")]
        public async Task<ActionResult> GetCurrentUserAsync()
        {
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = roles, Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id) });
            //new UserDTO { Id = user.Id, Name=user.Name, Surname=user.Surname, Email = user.NormalizedEmail}
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
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
                    await _logService.AddLog(new Log(user, $"Password changed successfully"));
                    return Ok(new MessageDTO { Message = "Password successfully changed" });

                }
                //await _logService.AddLog(new Log(user, $"Password does not changed - password does not meet the requirements"));
                return BadRequest(new ResponseErrorDTO
                {
                    Error = string.Join(" ", response.Errors.Select(e => e.Description))
                });
            }
            catch (Exception e)
            {
                //await _logService.AddLog(new Log(user, $"Error while changing password"));
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpGet("newsletter/join")]
        public async Task<ActionResult> SubscribeToNewsletter()
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(email));
                _userService.JoinNewsletter(user.Id);
                return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = roles, Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id) });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpGet("newsletter/leave")]
        public async Task<ActionResult> UnsubscribeToNewsletter()
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(email));
                _userService.LeaveNewsletter(user.Id);
                return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = roles, Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id) });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }
    
        
        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpDelete("delete-account")]
        public async Task<ActionResult> DeleteUser()
        {
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            await _userService.PreparingUserToBlock(user.Id);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Parse("2038-01-19 00:00:00"));//to jest maksimum dla timestampu xD
            await _logService.AddLog(new Log(user, $"Lock user due to deleting account"));
            return Ok();
        }


        //-------------------------------------------------employer functions

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
        [HttpGet()]
        public async Task<ActionResult> GetUsers(string filter, [FromQuery] PaggingParam pp)
        {
            var users = _userService.GetJustUsers(filter?.ToUpper()).Where(u => u.Email != HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value);
            List<UserDTO> usersDTO = new();
            foreach (var user in users.Skip((pp.Page - 1) * pp.Count).Take(pp.Count))
            {
                usersDTO.Add(new UserDTO { Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user), 
                    Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id), 
                    isLocked = user.LockoutEnd.HasValue ? DateTimeOffset.Compare(user.LockoutEnd.Value, DateTime.Now) > 0 : false }); ;
            }
            return Ok(new UsersWithPagging { TotalCount = users.Count(), Items = usersDTO });
        }


        //-------------------------------------------------admin functions

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono użytkownika!" });
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = roles, Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id), isLocked = user.LockoutEnd.HasValue ? DateTimeOffset.Compare(user.LockoutEnd.Value, DateTime.Now) > 0 : false });
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult> BlockUserByAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono użytkownika!" });
            await _userService.PreparingUserToBlock(user.Id);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Parse("2038-01-19 00:00:00"));//to jest maksimum dla timestampu xD
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var admin = await _userManager.FindByEmailAsync(email);
            await _logService.AddLog(new Log(admin, $"Lock user {user.Id} due to deleting account"));
            //await _userManager.DeleteAsync(user);
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = await _userManager.GetRolesAsync(user), Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id), isLocked = user.LockoutEnd.HasValue ? DateTimeOffset.Compare(user.LockoutEnd.Value, DateTime.Now) > 0 : false });
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPatch("admin/{id}")]
        public async Task<ActionResult> UnblockUserByAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono użytkownika!" });
            await _userManager.SetLockoutEndDateAsync(user, null);
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var admin = await _userManager.FindByEmailAsync(email);
            await _logService.AddLog(new Log(admin, $"Unlock user {user.Id}"));
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = await _userManager.GetRolesAsync(user), Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id), isLocked = user.LockoutEnd.HasValue ? DateTimeOffset.Compare(user.LockoutEnd.Value, DateTime.Now) > 0 : false });
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/add-role")]
        public async Task<ActionResult> AddRoles(int id, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono użytkownika!" });
            foreach(var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            if ((await _userManager.IsLockedOutAsync(user)) && (await _userManager.GetRolesAsync(user)).Any())
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
                await _logService.AddLog(new Log(user, $"Unlock user {user.Id} due to existing roles"));
            }
            await _logService.AddLog(new Log(user, $"Add roles {roles.Aggregate((i, j) => i + ", " + j)} to {user.Id}"));
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = await _userManager.GetRolesAsync(user), Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id), isLocked = user.LockoutEnd.HasValue ? DateTimeOffset.Compare(user.LockoutEnd.Value, DateTime.Now) > 0 : false });
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/remove-role")]

        public async Task<ActionResult> RemoveRole(int id, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono użytkownika!" });
            await _userManager.RemoveFromRolesAsync(user, roles);
            if (!(await _userManager.GetRolesAsync(user)).Any())
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Parse("2038-01-19 00:00:00"));//to jest maksimum dla timestampu xD
                await _logService.AddLog(new Log(user, $"Lock user {user.Id} due to removing last role"));
            }
            await _logService.AddLog(new Log(user, $"Remove roles {roles.Aggregate((i, j) => i + ", " + j)} from {user.Id}"));
            return Ok(new UserDTO { Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email, Roles = await _userManager.GetRolesAsync(user), Newsletter = await _userService.CheckIfUserIsSubscribed(user.Id), isLocked = user.LockoutEnd.HasValue ? DateTimeOffset.Compare(user.LockoutEnd.Value, DateTime.Now) > 0 : false });
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPatch("{id}/update-user")]
        public async Task<ActionResult> ChangeUserDetailsByAdmin(int id, UserDetailsDTO userDetailsDTO)
        {
            try
            {
                var user = _userService.UpdateUser(id, userDetailsDTO);
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(id.ToString()));
                user.Roles = roles;
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var adminUser = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(adminUser.Id, $"Changed user details"));
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("newsletter/send")]
        public async Task<ActionResult> SendNewsletter(NewsletterInfoDTO newsletter)
        {
            try
            {
                var users = await _userService.GetNewsletterSubscribers(newsletter.TournamentId);
                foreach(var user in users)
                {
                    await _emailService.SendAsync(user.Email, newsletter.Title, newsletter.Body);
                }
                return Ok(new MessageDTO { Message = "Newsletter został wysłany!" });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }

    }
}
