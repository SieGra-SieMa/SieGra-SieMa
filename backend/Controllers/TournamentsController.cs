using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ContestDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.GroupDTO;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.DTOs.TeamInTournamentDTO;
using SieGraSieMa.DTOs.TeamsDTO;
using SieGraSieMa.DTOs.TournamentDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using SieGraSieMa.Services.Interfaces;
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
        private readonly IContestService _contestService;
        private readonly ITeamService _teamService;


        private readonly UserManager<User> _userManager;

        //private readonly IMapper _mapper;

        public TournamentsController(ITournamentsService tournamentsService, UserManager<User> userManager, IContestService contestService, ITeamService teamService)
        {
            _tournamentsService = tournamentsService;
            //_mapper = mapper;
            _userManager = userManager;
            _contestService = contestService;
            _teamService = teamService;
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
            try
            {
                //var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };
                var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Address = tournament.Address };
                var result = await _tournamentsService.CreateTournament(newTournament);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTournament(RequestTournamentDTO tournament, int id)
        {
            try
            {
                //var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };
                var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Address = tournament.Address };

                var result = await _tournamentsService.UpdateTournament(id, newTournament);

                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var result = await _tournamentsService.DeleteTournament(id);

            if (!result)
                return NotFound(new ResponseErrorDTO { Error = "Tournament not found" });

            return Ok(new MessageDTO { Message = $"Tournament with {id} id succesfully deleted" });
        }


        [AllowAnonymous]
        [HttpGet("{id}/summary")]
        public async Task<IActionResult> GetSummary(int id)
        {
            var summary = await _tournamentsService.GetSummary(id);

            if (summary == null)
                return NotFound(new ResponseErrorDTO { Error = "Summary not found" });

            return Ok(new MessageDTO { Message = summary });
        }
        [HttpPatch("{id}/summary")]
        public async Task<IActionResult> SetSummary(int id, RequestTournamentDescription dto)
        {
            try
            {
                var response = await _tournamentsService.SetSummary(id, dto.data);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}/description")]
        public async Task<IActionResult> GetDescription(int id)
        {
            var desc = await _tournamentsService.GetDescription(id);

            if (desc == null)
                return NotFound(new ResponseErrorDTO { Error = "Description not found" });

            return Ok(new MessageDTO { Message = desc });
        }
        [HttpPatch("{id}/description")]
        public async Task<IActionResult> SetDescription(int id, RequestTournamentDescription dto)
        {
            try
            {
                var response = await _tournamentsService.SetDescription(id, dto.data);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }


        [AllowAnonymous]
        [HttpGet("{id}/teams/count")]
        public async Task<IActionResult> CountTeams(int id, [FromQuery] ITournamentsService.TeamPaidEnum filter)
        {
            var response = await _tournamentsService.CheckCountTeamsInTournament(id, filter);
            if (response == 0) return BadRequest(new { message = "Bad tournament number or no teams registered for tournament" });

            return Ok(new { count = response });
        }
        [HttpGet("{id}/teams")]
        public async Task<IActionResult> GetTeamsInTournament(int id, [FromQuery] ITournamentsService.TeamPaidEnum filter)
        {
            var response = await _tournamentsService.GetTeamsInTournament(id, filter);
            return Ok(response);
        }
        [HttpPatch("{id}/teams/{teamId}")]
        public async Task<IActionResult> SetPaidStatusTeamsInTournament(int id, int teamId, [FromQuery] ITournamentsService.TeamPaidEnum filter)
        {
            try
            {
                var response = await _tournamentsService.SetPaidStatusTeamsInTournament(id, teamId, filter);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        [HttpGet("{id}/teams/checkCorrectness")]
        public async Task<IActionResult> CheckCorectnessOfTeams(int id)
        {
            try
            {
                var response = await _tournamentsService.CheckCorectnessOfTeams(id); ;
                if (response.Any()) return BadRequest(response);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        [HttpPost("{id}/teams/join")]
        public async Task<IActionResult> JoinTournament(int id, int teamId)
        {
            try
            {
                var team = _teamService.GetTeamWithPlayers(teamId);
                if (team == null)
                    return BadRequest(new ResponseErrorDTO { Error = "Team does not exists" });
                List<User> listOfUsers = new();
                //team.Players.Select(async p => listOfUsers.Add(await _userManager.FindByIdAsync(p.UserId.ToString()))); zmienilem na foreach ponizej
                team.Players.ToList().ForEach(async p =>
                {
                    listOfUsers.Add(await _userManager.FindByIdAsync(p.UserId.ToString()));
                });
                var respone = await _tournamentsService.CheckUsersInTeam(listOfUsers, id);
                if (respone)
                {
                    var resp = await _tournamentsService.AddTeamToTournament(team.Id, id);
                    if (!resp)
                        return BadRequest(new ResponseErrorDTO { Error = "Tournament does not exists" });

                    return Ok();
                }

                return BadRequest(new ResponseErrorDTO { Error = "One of the players already belongs to another team" });
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


        [AllowAnonymous]
        [HttpGet("{id}/contests")]
        public async Task<IActionResult> GetContests(int id)
        {
            var contests = await _contestService.GetContests(id);
            return Ok(contests);
        }
        [AllowAnonymous]
        [HttpGet("{id}/contests/{contestId}")]
        public async Task<IActionResult> GetContest(int contestId)
        {
            var contest = await _contestService.GetContest(contestId);

            if (contest == null)
                return NotFound(new ResponseErrorDTO { Error = "Contest not found" });

            return Ok(contest);
        }
        [HttpPost("{id}/contests")]
        public async Task<IActionResult> CreateContest(RequestContestDTO contest, int id)
        {
            var newContest = new Contest { Name = contest.Name, TournamentId = id };
            var result = await _contestService.CreateContest(newContest);

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }
        [HttpPatch("{id}/contests/{contestId}")]
        public async Task<IActionResult> UpdateContest(RequestContestDTO contest, int id, int contestId)
        {
            var newContest = new Contest { Name = contest.Name, TournamentId = id };

            var result = await _contestService.UpdateContest(contestId, newContest);

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }
        [HttpDelete("{id}/contests/{contestId}")]
        public async Task<IActionResult> DeleteContest(int contestId)
        {
            var result = await _contestService.DeleteContest(contestId);
            if (!result) return NotFound(new ResponseErrorDTO { Error = "Contest not found" });
            return Ok(result);
        }
        [HttpPost("{id}/contests/{contestId}/setScore")]
        public async Task<IActionResult> AddContestant(int contestId, AddContestantDTO addContestantDTO)
        {
            var result = await _contestService.SetScore(contestId, addContestantDTO);
            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });
            return Ok();
        }
    }
}
