using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.MatchDTO;
using SieGraSieMa.Services.Tournaments;
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
        private readonly ITournamentsService _tournamentsService;

        public MatchController(ITournamentsService tournamentService)
        {
            _tournamentsService = tournamentService;
        }

        [HttpPatch("insertMatchResults")]
        public async Task<IActionResult> InsertMatchResult(MatchResultDTO matchResultDTO)
        {
            try
            {
                var response=await _tournamentsService.InsertMatchResult(matchResultDTO);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
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
                return BadRequest(e);
            }
        }
        [HttpGet("getLadderMatches/{tournamentId}")]
        public async Task<IActionResult> GetLadderMatches(int tournamentId)
        {
            try
            {
                var response = await _tournamentsService.GetLadderMatches(tournamentId);
                if (!response.Phases.Any()) return NotFound(new ResponseErrorDTO { Error = "No matches found for this ladder" });
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        
    }
}
