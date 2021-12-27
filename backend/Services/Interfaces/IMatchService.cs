using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Interfaces
{
    public interface IMatchService
    {
        int CheckCountTeamsInTournament(int tournamentId);
        int CheckCountPaidTeamsInTournament(int tournamentId);
        IEnumerable<Team> CheckCorectnessOfTeams(int tournamentId);
        IEnumerable<Group> CreateBasicGroups(int tournamentId);
        IEnumerable<Group> CreateLadderGroups(int tournamentId, int groupCount);
        IEnumerable<TeamInGroup> AddTeamsToGroup(int tournamentId);
        IEnumerable<TeamInGroup> CreateTeamTemplatesInLadder(int tournamentId);
        IEnumerable<Match> CreateMatchTemplates(int tournamentId);
        Match InsertMatchResult(int matchId);
        IEnumerable<Group> ComposeLadderGroups(int tournamentId);


    }
}
