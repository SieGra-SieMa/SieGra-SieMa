using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ContestantDTO;
using SieGraSieMa.DTOs.ContestDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.GroupDTO;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.DTOs.TeamInTournamentDTO;
using SieGraSieMa.DTOs.TeamsDTO;
using SieGraSieMa.DTOs.TournamentDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static SieGraSieMa.Services.IMediaService;

namespace SieGraSieMa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentsService _tournamentsService;
        private readonly IContestService _contestService;
        private readonly ITeamService _teamService;
        private readonly IAlbumService _albumService;
        private readonly IMediaService _mediaService;
        private readonly ILogService _logService;

        private readonly UserManager<User> _userManager;

        //private readonly IMapper _mapper;

        public TournamentsController(ITournamentsService tournamentsService, UserManager<User> userManager, IContestService contestService, ITeamService teamService, IAlbumService albumService, IMediaService mediaService, ILogService logService)
        {
            _tournamentsService = tournamentsService;
            //_mapper = mapper;
            _userManager = userManager;
            _contestService = contestService;
            _teamService = teamService;
            _albumService = albumService;
            _mediaService = mediaService;
            _logService = logService;
        }

        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetTournaments()
        {
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var user = email != null ? await _userManager.FindByEmailAsync(email) : null;
            var tournaments = await _tournamentsService.GetTournaments(user);

            return Ok(tournaments);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournament(int id)
        {
            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var user = email != null ? await _userManager.FindByEmailAsync(email) : null;
            var tournament = await _tournamentsService.GetTournament(id, user);

            if (tournament == null)
                return NotFound(new ResponseErrorDTO { Error = "Tournament not found" });

            return Ok(tournament);
        }

        /*[AllowAnonymous]
        [HttpGet("{id}/description")]
        public async Task<IActionResult> GetDescription(int id)
        {
            var desc = await _tournamentsService.GetDescription(id);

            if (desc == null)
                return NotFound(new ResponseErrorDTO { Error = "Description not found" });

            return Ok(new MessageDTO { Message = desc });
        }*/

        [AllowAnonymous]
        [HttpGet("{id}/teams")]
        public async Task<IActionResult> GetTeamsInTournament(int id, [FromQuery] ITournamentsService.TeamPaidEnum filter)
        {
            var response = await _tournamentsService.GetTeamsInTournament(id, filter);
            return Ok(response);
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

        [AllowAnonymous]
        [HttpGet("{id}/albums")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            try
            {
                var result = await _tournamentsService.GetTournamentWithAlbums(id);
                if (result == null)
                    return BadRequest(new ResponseErrorDTO { Error = "Tournament not found!" });

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        //-------------------------------------------------users functions

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("{id}/teams/join")]
        public async Task<IActionResult> JoinTournament(int id, int teamId)
        {
            try
            {
                var team = _teamService.GetTeamWithPlayers(teamId);
                if (team == null)
                    return BadRequest(new ResponseErrorDTO { Error = "Team does not exists" });
                List<User> listOfUsers = new();
                foreach (var player in team.Players) listOfUsers.Add(await _userManager.FindByIdAsync(player.UserId.ToString()));
                var response = await _tournamentsService.CheckUsersInTeam(listOfUsers, id);
                if (response)
                {
                    var response2 = await _tournamentsService.AddTeamToTournament(team.Id, id);
                    if (!response2)
                        return BadRequest(new ResponseErrorDTO { Error = "Tournament does not exists" });
                    var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                    var user = await _userManager.FindByEmailAsync(email);
                    await _logService.AddLog(new Log(user, $"Team with id {teamId} was added to tournament with id {id}"));
                    return Ok();
                }

                return BadRequest(new ResponseErrorDTO { Error = "One of the players already belongs to another team" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "EveryOneAuthenticated")]
        [HttpPost("{id}/teams/leave")]
        public async Task<IActionResult> LeaveTournament(int id, int teamId)
        {
            try
            {
                var team = _teamService.GetTeamWithPlayers(teamId);
                if (team == null) return BadRequest(new ResponseErrorDTO { Error = "Team does not exists" });

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                if(team.CaptainId != user.Id)
                    return BadRequest(new ResponseErrorDTO { Error = "You are not the captain of this team!" });

                var resp = await _tournamentsService.RemoveTeamFromTournament(team.Id, id);

                
                await _logService.AddLog(new Log(user, $"Team with id {teamId} was removed from tournament with id {id}"));

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
        

        //-------------------------------------------------employee functions

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
        [HttpGet("{id}/teams/count")]
        public async Task<IActionResult> CountTeams(int id, [FromQuery] ITournamentsService.TeamPaidEnum filter)
        {
            var response = await _tournamentsService.CheckCountTeamsInTournament(id, filter);
            if (response == 0) return BadRequest(new { message = "Bad tournament number or no teams registered for tournament" });

            return Ok(new { count = response });
        }

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
        [HttpPatch("{id}/teams/{teamId}")]
        public async Task<IActionResult> SetPaidStatusTeamsInTournament(int id, int teamId, [FromQuery] ITournamentsService.TeamPaidEnum filter)
        {
            try
            {
                var response = await _tournamentsService.SetPaidStatusTeamsInTournament(id, teamId, filter);
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Team with id {teamId} has been marked as {filter} in tournament with id {id}"));
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
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

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
        [HttpPost("{id}/composeLadder")]
        public async Task<IActionResult> ComposeLadderGroups(int id)
        {
            try
            {
                await _tournamentsService.ComposeLadderGroups(id);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = email != null ? await _userManager.FindByEmailAsync(email) : null;
                var tournament = await _tournamentsService.GetTournament(id, user);
                await _logService.AddLog(new Log(user, $"Ladder was composed in tournament with id {id}"));

                return Ok(tournament);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
        [HttpPost("{id}/resetLadder")]
        public async Task<IActionResult> ResetLadder(int id)
        {
            try
            {
                await _tournamentsService.ResetLadder(id);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = email != null ? await _userManager.FindByEmailAsync(email) : null;
                var tournament = await _tournamentsService.GetTournament(id, user);
                await _logService.AddLog(new Log(user, $"Ladder was reset in tournament with id {id}"));

                return Ok(tournament);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyEmployeesAuthenticated")]
        [HttpPost("{id}/contests/{contestId}/setScore")]
        public async Task<IActionResult> AddContestant(int contestId, AddContestantDTO addContestantDTO)
        {
            try
            {
                ResponseContestantDTO result;
                if (addContestantDTO.Points <= 0)
                {
                    result = await _contestService.DeleteScore(contestId, addContestantDTO);
                }
                else
                {
                    result = await _contestService.SetScore(contestId, addContestantDTO);
                }
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Set score in contest with id {contestId} for {addContestantDTO.Email}" + ((addContestantDTO.Points > 0) ? "" : $", set score to 0 to deleting the score")));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        //-------------------------------------------------admin functions

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost()]
        public async Task<IActionResult> CreateTournament(RequestTournamentDTO tournament)
        {
            try
            {
                var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };
                var result = await _tournamentsService.CreateTournament(newTournament);
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Tournament with id {result.Id} created"));
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTournament(RequestTournamentDTO tournament, int id)
        {
            try
            {
                var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };

                var result = await _tournamentsService.UpdateTournament(id, newTournament);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Tournament with id {id} updated"));

                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var result = await _tournamentsService.DeleteTournament(id);

            if (!result)
                return NotFound(new ResponseErrorDTO { Error = "Tournament not found" });

            var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            await _logService.AddLog(new Log(user, $"Tournament with id {id} deleted"));

            return Ok(new MessageDTO { Message = $"Tournament with {id} id successfully deleted" });
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPatch("{id}/description")]
        public async Task<IActionResult> SetDescription(int id, RequestTournamentDescription dto)
        {
            try
            {
                var response = await _tournamentsService.SetDescription(id, dto.data);
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Tournament with id {id} got new description"));
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/prepareTournament")]
        public async Task<IActionResult> PrepareTournament(int id)
        {
            try
            {
                var groups = await _tournamentsService.CreateBasicGroups(id);
                var teams = await _tournamentsService.AddTeamsToGroup(id);
                var matches = await _tournamentsService.CreateMatchTemplates(id);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = email != null ? await _userManager.FindByEmailAsync(email) : null;
                var tournament = await _tournamentsService.GetTournament(id, user);

                await _logService.AddLog(new Log(user, $"Tournament with id {id} has prepared"));
                return Ok(tournament);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/resetTournament")]
        public async Task<IActionResult> ResetTournament(int id)
        {
            try
            {
                await _tournamentsService.ResetTournament(id);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = email != null ? await _userManager.FindByEmailAsync(email) : null;
                var tournament = await _tournamentsService.GetTournament(id, user);
                await _logService.AddLog(new Log(user, $"Tournament with id {id} has reset"));

                return Ok(tournament);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/contests")]
        public async Task<IActionResult> CreateContest(RequestContestDTO contest, int id)
        {
            try
            {
                var newContest = new Contest { Name = contest.Name, TournamentId = id };
                var result = await _contestService.CreateContest(newContest);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"New contest with name {contest.Name} in tournament with id {id}"));

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
            
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPatch("{id}/contests/{contestId}")]
        public async Task<IActionResult> UpdateContest(RequestContestDTO contest, int id, int contestId)
        {
            try
            {
                var newContest = new Contest { Name = contest.Name, TournamentId = id };

                var result = await _contestService.UpdateContest(contestId, newContest);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Contest with id {contestId} in tournament with id {id} was updated"));

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpDelete("{id}/contests/{contestId}")]
        public async Task<IActionResult> DeleteContest(int id, int contestId)
        {
            try
            {
                var result = await _contestService.DeleteContest(contestId);
                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Contest with id {contestId} in tournament with id {id} was deleted"));
                return Ok(new MessageDTO { Message= $"Contest with name {result.Name} was deleted" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
            
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/albums")]
        public async Task<IActionResult> CreateAlbum(int id, RequestAlbumDTO album)
        {
            try
            {
                var result = await _albumService.CreateAlbum(new Album { CreateDate = album.CreateDate, Name = album.Name, TournamentId = id });

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Album {album.Name} created in tournament with id {id}"));
                return Ok(new ResponseAlbumWithoutMediaDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    CreateDate = result.CreateDate,
                    ProfilePicture = result.MediumInAlbums.Select(m => m.Medium.Url).FirstOrDefault()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/add-profile-photo")]
        public async Task<IActionResult> AddPhoto(int id, IFormFile[] file)
        {
            try
            {
                var tournament = await _tournamentsService.GetTournament(id, null);
                if (tournament == null)
                    return NotFound("Tournament not found!");

                if (file.Length != 1)
                    return BadRequest("There should be only one photo sent!");

                var list = await _mediaService.CreateMedia(null, tournament.Id, file, MediaTypeEnum.tournaments);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Media {list[0].Url} set as profile picture of tournament with id {id}"));

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }

        }
    }
}
