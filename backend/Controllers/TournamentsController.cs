using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Tournaments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentsService _tournamentsService;

        public TournamentsController(ITournamentsService tournamentsService)
        {
            _tournamentsService = tournamentsService;
        }

        [AllowAnonymous]
        [HttpGet("All")]
        public async Task<IActionResult> GetTournaments()
        {
            var tournaments = await _tournamentsService.GetTournaments();

            return Ok(tournaments);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournament(int id)
        {
            var tournament = await _tournamentsService.GetTournament(id);

            if (tournament == null)
                return NotFound();

            return Ok(tournament);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTournament(Tournament tournament)
        {
            var result = await _tournamentsService.CreateTournament(tournament);

            if (!result)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTournament(Tournament tournament)
        {
            var result = await _tournamentsService.UpdateTournament(tournament);

            if (!result)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var result = await _tournamentsService.DeleteTournament(id);

            if (!result)
                return NotFound(result);

            return Ok(result);
        }



    }
}
