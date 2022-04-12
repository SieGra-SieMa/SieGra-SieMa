using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public class TeamService : ITeamService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public TeamService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }

        public Team CreateTeam(string name, User captain)
        {
            var team = new Team() {
                Name = name,
                Captain = captain,
                Code = GenerateNewCode()
            };
            team.Players.Add(new Player()
            {
                Team=team,
                User=captain
            });
            _SieGraSieMaContext.Teams.Add(team);
            _SieGraSieMaContext.SaveChanges();

            return team;
        }

        public void JoinTeam(string code, User user) {
            var isExist = _SieGraSieMaContext.Teams.Where(e => e.Code == code)
                .SingleOrDefault();

            if (isExist == null) {
                throw new Exception("Team with this code doesn`t exist");
            }

            isExist.Players.Add(new Player()
            {
                Team = isExist,
                User = user
            });
            _SieGraSieMaContext.SaveChanges();
        }

        public void LeaveTeam(int id, User user)
        {
            var team = _SieGraSieMaContext.Teams
                .Include(e => e.Players)
                .ThenInclude(e => e.User)
                .Where(e => e.Id == id)
                .SingleOrDefault();

            if (team == null)
            {
                throw new Exception("Team with this id doesn`t exist");
            }

            var player = team.Players
                .Where(e => e.UserId == user.Id && e.TeamId == team.Id)
                .SingleOrDefault();

            team.Players.Remove(player);

            if (team.CaptainId == user.Id)
            {
                var newCaptain = team.Players.FirstOrDefault()?.User;
                if (newCaptain != null)
                {
                    team.Captain = newCaptain;
                }
                else
                {
                    _SieGraSieMaContext.Teams.Remove(team);
                }
            }

            _SieGraSieMaContext.SaveChanges();
        }

        public void ChangeCaptain(int Id, int UserId)
        {
            var Team = GetTeam(Id);
            Team.CaptainId = UserId;
            _SieGraSieMaContext.SaveChanges();
        }

        public void ChangeName(int Id, string Name)
        {
            var Team = GetTeam(Id);
            Team.Name = Name;
            _SieGraSieMaContext.SaveChanges();
        }

        public Team GetTeam(int Id)
        {
            return _SieGraSieMaContext.Teams.Where(t => t.Id == Id).SingleOrDefault();
        }

        public IEnumerable<GetTeamsDTO> GetTeamsWithUser(string email)
        {
            return _SieGraSieMaContext.Teams
                .Where(e => e.Players.Any(e => e.User.Email == email))
                .Include(e => e.Players)
                .ThenInclude(e => e.User).Select(t => new GetTeamsDTO{ Id=t.Id,Name=t.Name,CaptainId=t.CaptainId,CaptainName=t.Captain.Name,CaptainSurname=t.Captain.Surname,Code=t.Code})
                .ToList();
        }

        public IEnumerable<Team> GetTeams()
        {
            return _SieGraSieMaContext.Teams.ToList();
        }

        public void RemoveTeam(int Id)
        {
            _SieGraSieMaContext.Teams.Remove(GetTeam(Id));
            _SieGraSieMaContext.SaveChanges();
        }

        public string GenerateNewCode()
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 5)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
