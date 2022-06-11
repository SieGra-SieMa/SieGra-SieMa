using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.MatchDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly ITournamentsService _tournamentsService;

        private readonly UserManager<User> _userManager;

        public MatchController(ITournamentsService tournamentService, UserManager<User> userManager)
        {
            _tournamentsService = tournamentService;
            _userManager = userManager;
        }

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
        [HttpPatch("insertMatchResults")]
        public async Task<IActionResult> InsertMatchResult(MatchResultDTO matchResultDTO)
        {
            try
            {
                await _tournamentsService.InsertMatchResult(matchResultDTO);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = email != null ? await _userManager.FindByEmailAsync(email) : null;
                var tournament = await _tournamentsService.GetTournament(matchResultDTO.TournamentId, user);

                return Ok(tournament);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("getGroupMatches/{tournamentId}")]
        public async Task<IActionResult> GetAvailableMatchesInGroup(int tournamentId,[FromQuery]ITournamentsService.MatchesEnum filter)
        {
            try
            {
                var response = await _tournamentsService.GetAvailableGroupMatches(tournamentId, filter);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("getLadderMatches/{tournamentId}")]
        public async Task<IActionResult> GetLadderMatches(int tournamentId)
        {
            try
            {
                var response = await _tournamentsService.GetLadderMatches(tournamentId);
                if (!response.Any()) return NotFound(new ResponseErrorDTO { Error = "No matches found for this ladder" });
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        
        
    }
}
