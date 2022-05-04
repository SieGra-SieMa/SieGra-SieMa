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
                if (!response.Any()) return Ok();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
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
                return BadRequest(e);
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
                return BadRequest(e);
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
                return BadRequest(e);
            }
        }
        [HttpPatch("insertMatchResults")]
        public async Task<IActionResult> InsertMatchResult(MatchResultDTO matchResultDTO)
        {
            try
            {
                var response=await _matchService.InsertMatchResult(matchResultDTO);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
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
                return BadRequest(e);
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
                return BadRequest(e);
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
                return BadRequest(e);
            }
        }
        [HttpGet("getLadderMatches/{tournamentId}")]
        public async Task<IActionResult> GetLadderMatches(int tournamentId)
        {
            try
            {
                var response = await _matchService.GetLadderMatches(tournamentId);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("composeLadderGroups/{tournamentId}")]
        public async Task<IActionResult> ComposeLadderGroups(int tournamentId)
        {
            try
            {
                var response = await _matchService.ComposeLadderGroups(tournamentId);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
