using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.MatchDTO;
using SieGraSieMa.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    //[Authorize(Roles ="Emp")]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("countTeams/{tournamentId}")]
        public async Task<IActionResult> CountTeams(int tournamentId)
        {
            var response = await _matchService.CheckCountTeamsInTournament(tournamentId);
            if (response == 0) return BadRequest(new { message = "Bad tournament number or no teams registered for tournament" });

            return Ok(response);
        }

        [HttpGet("countPaidTeams/{tournamentId}")]
        public async Task<IActionResult> CountPaidTeams(int tournamentId)
        {
            var response = await _matchService.CheckCountPaidTeamsInTournament(tournamentId);
            if (response == 0) return BadRequest(new { message = "Bad tournament number or no teams paid for tournament" });
            return Ok(response);
        }

        [HttpGet("checkCorectnessOfTeams/{tournamentId}")]
        public async Task<IActionResult> CheckCorectnessOfTeams(int tournamentId)
        {
            try
            {
                var response = await _matchService.CheckCorectnessOfTeams(tournamentId);
                //if (response.Count() == 0) return Ok();
                if (response.Any()) return Ok();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("createBasicGroups/{tournamentId}")]
        public async Task<IActionResult> CreateBasicGroups(int tournamentId)
        {
            try
            {
                var response = await _matchService.CreateBasicGroups(tournamentId);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("addTeamsToGroups/{tournamentId}")]
        public async Task<IActionResult> AddTeamsToGroup(int tournamentId)
        {
            try
            {
                var response = await _matchService.AddTeamsToGroup(tournamentId);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("createMatchTemplates/{tournamentId}")]
        public async Task<IActionResult> CreateMatchTemplates(int tournamentId)
        {
            try
            {
                var response = await _matchService.CreateMatchTemplates(tournamentId);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPatch("insertMatchResults")]
        public async Task<IActionResult> InsertMatchResult(MatchResultDTO matchResultDTO)
        {
            try
            {
                /*var identity = HttpContext.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claim = identity.Claims;
                var email = claim.Where(e => e.Type == ClaimTypes.Name).First().Value;
                _userService.UpdateUser(email, userDetailsDTO);*/
                var response=await _matchService.InsertMatchResult(matchResultDTO);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAvailableGroupMatches/{tournamentId}")]
        public async Task<IActionResult> GetAvailableGroupMatches(int tournamentId)
        {
            try
            {
                var response = await _matchService.GetAvailableGroupMatches(tournamentId, IMatchService.MatchesEnum.All);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAvailableGroupMatchesPlayed/{tournamentId}")]
        public async Task<IActionResult> GetAvailableGroupMatchesPlayed(int tournamentId)
        {
            try
            {
                var response = await _matchService.GetAvailableGroupMatches(tournamentId, IMatchService.MatchesEnum.Played);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("getAvailableGroupMatchesNotPlayed/{tournamentId}")]
        public async Task<IActionResult> GetAvailableGroupMatchesNotPlayed(int tournamentId)
        {
            try
            {
                var response = await _matchService.GetAvailableGroupMatches(tournamentId, IMatchService.MatchesEnum.NotPlayed);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /*
        IEnumerable<Group> CreateLadderGroups(int tournamentId); wydaje mi się żeby nie robić, bo to się odpala automatycznie z basic group
        IEnumerable<TeamInGroup> CreateTeamTemplatesInLadder(int tournamentId);
        
        Match InsertMatchResult(int matchId);
        IEnumerable<Group> ComposeLadderGroups(int tournamentId);*/
    }
}
