using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {
        private readonly IGenerateService _generateService;

        public GenerateController(IGenerateService generateService)
        {
            _generateService = generateService;
        }
        [HttpGet("matchData")]
        public async Task<IActionResult> GenerateMatchData()
        {
            var teams = await _generateService.GenerateTeams(20,1);
            if (teams == null) return NotFound();

            return Ok(teams);
        }
    }
}
