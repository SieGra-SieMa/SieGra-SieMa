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
        IEnumerable<GetTeamsDTO> GetTeamsWithUser(string email);
        IEnumerable<Team> GetTeams();
        /*Task<bool> CheckUsersInTeamAsync(List<User> users, int tournamentId);*/
    }
}
