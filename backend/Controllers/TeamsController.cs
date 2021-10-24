using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;

        public TeamsController(ITeamService teamService, IUserService userService)
        {
            _teamService = teamService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetTeamByMail()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claim = identity.Claims;
                var email = claim.Where(e => e.Type == ClaimTypes.Name).First().Value;
                return Ok(_teamService.GetTeamsWithUser(email));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //[HttpPost("create")]
        [HttpPost()]
        [Authorize(Roles = "User")]
        public IActionResult Create(TeamDTO teamDTO)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claim = identity.Claims;
                var email = claim.Where(e => e.Type == ClaimTypes.Name).First().Value;
                var captain = _userService.GetUser(email);
                return Ok(_teamService.CreateTeam(teamDTO.Name, captain));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("join")]
        [Authorize(Roles = "User")]
        public IActionResult Join(TeamCodeDTO teamCodeDTO)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claim = identity.Claims;
                var email = claim.Where(e => e.Type == ClaimTypes.Name).First().Value;
                var captain = _userService.GetUser(email);
                _teamService.JoinTeam(teamCodeDTO.Code, captain);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }
        [HttpPost("leave")]
        [Authorize(Roles = "User")]
        public IActionResult Leave(TeamLeaveDTO teamLeaveDTO)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claim = identity.Claims;
                var email = claim.Where(e => e.Type == ClaimTypes.Name).First().Value;
                var user = _userService.GetUser(email);
                _teamService.LeaveTeam(teamLeaveDTO.Id, user);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
