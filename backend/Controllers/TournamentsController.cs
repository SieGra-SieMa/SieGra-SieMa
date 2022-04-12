﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.TournamentDTO;
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

        private readonly IMapper _mapper;

        public TournamentsController(ITournamentsService tournamentsService, IMapper mapper)
        {
            _tournamentsService = tournamentsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet()]
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

        [HttpPost()]
        public async Task<IActionResult> CreateTournament(CreateTournamentDTO tournament)
        {
            //var newTournament = _mapper.Map<Tournament>(tournament);
            var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };
            var result = await _tournamentsService.CreateTournament(newTournament);

            if (!result)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTournament(CreateTournamentDTO tournament, int id)
        {
            var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };

            var result = await _tournamentsService.UpdateTournament(id, newTournament);

            if (!result)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var result = await _tournamentsService.DeleteTournament(id);

            if (!result)
                return NotFound(result);

            return Ok(result);
        }



    }
}
