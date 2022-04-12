using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("countTeams/{id}")]
        public async Task<IActionResult> CountTeams(int id)
        {
            var response = await _matchService.CheckCountTeamsInTournament(id);
            if (response == 0) return BadRequest(new { message = "Bad tournament number or no teams registered for tournament" });

            return Ok(response);
        }

        [HttpGet("countPaidTeams/{id}")]
        public async Task<IActionResult> CountPaidTeams(int id)
        {
            var response = await _matchService.CheckCountPaidTeamsInTournament(id);
            if (response == 0) return BadRequest(new { message = "Bad tournament number or no teams paid for tournament" });
            return Ok(response);
        }

        [HttpGet("checkCorectnessOfTeams/{id}")]
        public async Task<IActionResult> CheckCorectnessOfTeams(int id)
        {
            try
            {
                var response = await _matchService.CheckCorectnessOfTeams(id);
                if (response.Count() == 0) return Ok();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("createBasicGroups/{id}")]
        public async Task<IActionResult> CreateBasicGroups(int id)
        {
            try
            {
                var response = await _matchService.CreateBasicGroups(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("addTeamsToGroups/{id}")]
        public async Task<IActionResult> AddTeamsToGroup(int id)
        {
            var response = await _matchService.AddTeamsToGroup(id);
            return Ok(response);
        }

        /*
        IEnumerable<Group> CreateLadderGroups(int tournamentId); wydaje mi się żeby nie robić, bo to się odpala automatycznie z basic group
        IEnumerable<TeamInGroup> CreateTeamTemplatesInLadder(int tournamentId);
        IEnumerable<Match> CreateMatchTemplates(int tournamentId);
        Match InsertMatchResult(int matchId);
        IEnumerable<Group> ComposeLadderGroups(int tournamentId);*/
    }
}
