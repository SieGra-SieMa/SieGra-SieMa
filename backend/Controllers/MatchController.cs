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
        private readonly ILogService _logService;

        public MatchController(ITournamentsService tournamentService, UserManager<User> userManager, ILogService logService)
        {
            _tournamentsService = tournamentService;
            _userManager = userManager;
            _logService = logService;
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
                await _logService.AddLog(new Log(user, $"Match result inserted: tournament: { matchResultDTO.TournamentId }, phase: {matchResultDTO.Phase}, id: {matchResultDTO.MatchId}, homeScore: {matchResultDTO.HomeTeamPoints}, awayScore: {matchResultDTO.AwayTeamPoints}"));

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
        public async Task<IActionResult> GetAvailableMatchesInGroup(int tournamentId, [FromQuery] ITournamentsService.MatchesEnum filter)
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
                if (!response.Any()) return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono meczów dla tej drabinki!" });
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }


    }
}
