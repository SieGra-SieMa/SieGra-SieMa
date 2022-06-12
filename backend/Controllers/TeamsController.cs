﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.TeamsDTO;
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
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly IMediaService _mediaService;
        private readonly IEmailService _emailService;


        public TeamsController(ITeamService teamService, IUserService userService, IEmailService emailService, IMediaService mediaService)
        {
            _teamService = teamService;
            _userService = userService;
            _emailService = emailService;
            _mediaService = mediaService;
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpGet]
        public IActionResult GetTeamByMail()
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                return Ok(_teamService.GetTeamsWithUser(email));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        
        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpGet("teamsIAmCaptain")]
        public async Task<IActionResult> GetTeamByMailForCaptain()
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                return Ok(await _teamService.GetTeamsWhichUserIsCaptain(email));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost()]
        public IActionResult Create(TeamDTO teamDTO)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var captain = _userService.GetUser(email);
                return Ok(_teamService.CreateTeam(teamDTO.Name, captain));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("join")]
        public async Task<IActionResult> Join(TeamCodeDTO teamCodeDTO)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var captain = _userService.GetUser(email);
                var response = await _teamService.IsUserAbleToJoinTeam(captain, teamCodeDTO.Code);
                if(!response)
                    return BadRequest(new ResponseErrorDTO { Error = "Player already belongs to another team which is in the same tournament as this one" });
                _teamService.JoinTeam(teamCodeDTO.Code, captain);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
            
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("leave")]
        public IActionResult Leave(TeamLeaveDTO teamLeaveDTO)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                _teamService.LeaveTeam(teamLeaveDTO.Id, user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeTeamDetailsAsync(int id, TeamDetailsDTO teamDetailsDTO)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                var team = await _teamService.ChangeTeamDetails(user.Id, id, teamDetailsDTO);
                return Ok(team);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("{id}/remove-user/{userId}")]
        public async Task<IActionResult> RemoveUserFromTeam(int id, int userId)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                var team = await _teamService.DeleteUserFromTeam(userId, user.Id, id);
                return Ok(team);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("{id}/switch-captain/{userId}")]
        public async Task<IActionResult> SwitchCaptainAsync(int id, int userId)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                var team = await _teamService.SwitchCaptain(id, user.Id, userId);
                return Ok(team);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                await _teamService.DeleteTeam(id, user.Id);
                return Ok(new MessageDTO { Message = $"Team successfully deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("{id}/send-invite")]
        public async Task<IActionResult> SendInvite(int id, string emailAdress)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                //await _teamService.DeleteUserFromTeam(userId, user.Id, id);
                var team = _teamService.GetTeam(id);
                if (team == null)
                    return NotFound(new ResponseErrorDTO { Error = "Team not found" });
                if(team.CaptainId != user.Id)
                    return BadRequest(new ResponseErrorDTO { Error = "You are not a captain" });
                await _emailService.SendAsync(emailAdress, "Zaproszenie do zespołu SiegraSiema", $"Dołącz do naszego teamu, użyj tego kodu {team.Code} na stronie SiegraSiema");
                return Ok(new MessageDTO { Message = $"Mail already sent" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("{id}/add-profile-photo")]
        public async Task<IActionResult> AddPhoto(int id, IFormFile[] file)
        {
            try
            {
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = _userService.GetUser(email);
                var team = _teamService.GetTeam(id);
                if (team.CaptainId != user.Id)
                    return Unauthorized(new ResponseErrorDTO { Error = "You are not the captain!" });

                if (file.Length != 1)
                    return BadRequest("There should be only one photo sent!");

                var list = await _mediaService.CreateMedia(null, team.Id, file, IMediaService.MediaTypeEnum.teams);

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }


        //-------------------------------------------------admin functions

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteByAdminAsync(int id)
        {
            try
            {
                await _teamService.DeleteTeamByAdmin(id);
                return Ok(new MessageDTO { Message = $"Team successfully deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllTeamsByAdmin()
        {
            try
            {
                var result = await _teamService.GetAllTeams();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

    }
}
