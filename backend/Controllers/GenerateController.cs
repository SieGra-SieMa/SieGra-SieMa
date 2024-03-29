﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.GenerateDTO;
using SieGraSieMa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "OnlyAdminAuthenticated")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {
        private readonly IGenerateService _generateService;

        public GenerateController(IGenerateService generateService)
        {
            _generateService = generateService;
        }
        [HttpPost("generateTeamsForTournament")]
        public async Task<IActionResult> GenerateTeamsForTournament([FromBody] GenerateTeamsDTO generateTeamsDTO)
        {
            var teams = await _generateService.GenerateTeams(generateTeamsDTO.teamsCount, generateTeamsDTO.tournamentId);
            if (teams == null) return NotFound();

            return Ok(teams);
        }
        [HttpPost("generateMatchResults")]
        public async Task<IActionResult> GenerateMatchResults([FromBody] GenerateMatchResultDTO generateMatchResultDTO)
        {
            var matches = await _generateService.GenerateMatchResults(generateMatchResultDTO.tournamentId, generateMatchResultDTO.phase);
            if (!matches) return Ok("No matches found");

            return Ok(matches);
        }
        [HttpPost("resetDatabase")]
        public async Task<IActionResult> ResetDatabase()
        {
            await _generateService.SeedBasicData();
            return Ok();
        }
        [HttpPost("TestAsync")]
        public async Task<IActionResult> TestAsync()
        {
            var t1 =_generateService.TestAsync();
            var t2 = _generateService.TestAsync();
            var t3 = _generateService.TestAsync();
            var t4 = _generateService.TestAsync();
            await t1;
            await t2;
            await t3;
            await t4;
            return Ok();
        }
    }
}
