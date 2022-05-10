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
        [HttpGet("getGroupMatches/{tournamentId}")]
        public async Task<IActionResult> GetAvailableMatchesInGroup(int tournamentId,[FromQuery]IMatchService.MatchesEnum filter)
        {
            try
            {
                var response = await _matchService.GetAvailableGroupMatches(tournamentId, filter);
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
        
        
    }
}
