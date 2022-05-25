using SieGraSieMa.DTOs.TeamsDTO;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Interfaces
{
    public interface ITeamService
    {
        Team CreateTeam(string name, User captain);
        void JoinTeam(string code, User user);
        void LeaveTeam(int id, User user);
        void ChangeName(int id, string name);
        void RemoveTeam(int id);
        void ChangeCaptain(int id, int userId);
        Team GetTeam(int id);
        Team GetTeamWithPlayers(int id);
        IEnumerable<GetTeamsDTO> GetTeamsWithUser(string email);
        IEnumerable<Team> GetTeams();
        Task<bool> IsUserAbleToJoinTeam(User user, string code);
        Task ChangeTeamDetails(int userId, int teamId, TeamDetailsDTO teamDetailsDTO);
        Task DeleteUserFromTeam(int userId, int captainId, int teamId);
        Task SwitchCaptain(int teamId, int oldCaptainId, int newCaptainId);
        Task DeleteTeam(int teamId, int captainId);

    }
}
