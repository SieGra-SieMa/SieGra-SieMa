using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.GroupDTO;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.DTOs.TeamInTournamentDTO;
using SieGraSieMa.DTOs.TeamsDTO;
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

        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public TournamentsController(ITournamentsService tournamentsService, IMapper mapper, UserManager<User> userManager)
        {
            _tournamentsService = tournamentsService;
            _mapper = mapper;
            _userManager = userManager;
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
                return NotFound(new ResponseErrorDTO { Error = "Tournament not found" });

            return Ok(tournament);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateTournament(RequestTournamentDTO tournament)
        {
            //var newTournament = _mapper.Map<Tournament>(tournament);
            var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };
            var result = await _tournamentsService.CreateTournament(newTournament);

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTournament(RequestTournamentDTO tournament, int id)
        {
            var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };

            var result = await _tournamentsService.UpdateTournament(id, newTournament);

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var result = await _tournamentsService.DeleteTournament(id);

            if (!result)
                return NotFound(new ResponseErrorDTO { Error = "Tournament not found" });

            return Ok(result);
        }

        [HttpGet("{id}/teams/count")]
        public async Task<IActionResult> CountTeams(int id, [FromQuery] ITournamentsService.TeamsEnum filter)
        {
            var response = await _tournamentsService.CheckCountTeamsInTournament(id, filter);
            if (response == 0) return BadRequest(new { message = "Bad tournament number or no teams registered for tournament" });

            return Ok(new { count = response });
        }
        [HttpGet("{id}/teams/checkCorrectness")]
        public async Task<IActionResult> CheckCorectnessOfTeams(int id)
        {
            try
            {
                var response = await _tournamentsService.CheckCorectnessOfTeams(id);;
                if (response.Any()) return BadRequest(response);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        [HttpPost("{id}/prepareTournament")]
        public async Task<IActionResult> PrepareTournament(int id)
        {
            try
            {
                var groups = await _tournamentsService.CreateBasicGroups(id);
                var teams = await _tournamentsService.AddTeamsToGroup(id);
                var matches = await _tournamentsService.CreateMatchTemplates(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        [HttpPost("{id}/groups/composeLadder")]
        public async Task<IActionResult> ComposeLadderGroups(int id)
        {
            try
            {
                var response = await _tournamentsService.ComposeLadderGroups(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [HttpPost("{id}/teams/join")]
        public async Task<IActionResult> JoinTournament(int id, GetTeamsDTO team)
        {
            try
            {
                List<User> listOfUsers = new();
                team.Players.ForEach(async p => listOfUsers.Add(await _userManager.FindByIdAsync(p.Id.ToString())));
                var respone = await _tournamentsService.CheckUsersInTeam(listOfUsers, id);
                if(respone)
                {
                    var resp = await _tournamentsService.AddTeamToTournament(team.Id, id);
                    if(!resp)
                        return BadRequest(new ResponseErrorDTO { Error = "Team or tournament does not exists" });

                    return Ok("Tournament joined!");
                }

                return BadRequest(new ResponseErrorDTO { Error = "One of the players already belongs to another team" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
    }
}
