using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SieGraSieMa.DTOs.MatchDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SieGraSieMa.Services.IMatchService;

namespace SieGraSieMa.Services
{
    public interface IMatchService
    {
        public enum MatchesEnum { All, NotPlayed, Played }
        
        public Task<GetAvailableGroupMatchesDTO> GetAvailableGroupMatches(int tournamentId, MatchesEnum matchesEnum);
        public Task<Match> InsertMatchResult(MatchResultDTO matchResultDTO);
        public Task<GetLadderDTO> GetLadderMatches(int tournamentId);
    }
    public class MatchService : IMatchService
    {
        public IConfiguration Configuration { get; set; }
        private readonly SieGraSieMaContext _context;
        public MatchService(SieGraSieMaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        
        public async Task<Match> InsertMatchResult(MatchResultDTO DTO)
        {
            var match = _context.Matches.Find(DTO.TournamentId, DTO.Phase, DTO.MatchId);
            if (match == null) throw new Exception("No match found with this PKs");
            if (match.TeamHomeId == null || match.TeamAwayId == null) throw new Exception("Match has no teams");
            match.TeamHomeScore = DTO.HomeTeamPoints;
            match.TeamAwayScore = DTO.AwayTeamPoints;

            if (DTO.Phase != 0)//jeśli w drabince
            {
                int phases = _context.Groups.Where(g => g.TournamentId == DTO.TournamentId).Select(g => g.Phase).Max();
                if (DTO.Phase < phases - 1)//jeśli nie finał i nie 3 miejsce
                {
                    _context.Entry(match).Reference(m => m.TeamHome).Load();
                    _context.Entry(match).Reference(m => m.TeamAway).Load();
                    _context.Entry(match.TeamHome).Reference(tt => tt.Team).Load();
                    _context.Entry(match.TeamAway).Reference(tt => tt.Team).Load();
                    Team winner = DTO.HomeTeamPoints > DTO.AwayTeamPoints ? match.TeamHome.Team : match.TeamAway.Team;
                    /*Team winner = _context.Teams.Include(t => t.TeamInGroups)
                            .ThenInclude(t => DTO.HomeTeamPoints > DTO.AwayTeamPoints ? t.MatchTeamHomes : t.MatchTeamAways)
                            .Where(t => t.TeamInGroups.Any(tt => DTO.HomeTeamPoints > DTO.AwayTeamPoints ? tt.MatchTeamHomes.Contains(match) : tt.MatchTeamAways.Contains(match)))
                            .FirstOrDefault();*/

                    IQueryable<Match> query = _context.Matches.Include(m => m.TeamHome).Include(m => m.TeamAway)
                                        .Where(m => m.TournamentId == DTO.TournamentId);
                    if (DTO.Phase < phases - 2)//jeśli wcześniej niż półfinał
                    {
                        query = query.Where(m => m.Phase == DTO.Phase + 1 && m.MatchId == (int)Math.Ceiling(DTO.MatchId / 2.0));
                    }
                    else if (DTO.Phase == phases - 2)//jeśli półfinał
                    {
                        query = query.Where(m => m.Phase == DTO.Phase + 2 && m.MatchId == 1);
                    }

                    Match nextMatch = query.FirstOrDefault();
                    if (nextMatch.TeamHome.TeamId == null) nextMatch.TeamHome.Team = winner;
                    else if (nextMatch.TeamAway.TeamId == null) nextMatch.TeamAway.Team = winner;
                    /*nextMatch.TeamHome.Team = null;
                    nextMatch.TeamAway.Team = null;*/
                    //else throw new Exception("Trying update next match, which already have both teams");
                    //TODO: Zastanowić się nad końcówką pozwalającą ręcznie zaktualizować zespoły na wypadek błędu podczas wpisywania
                    _context.Matches.Update(nextMatch);

                    if (DTO.Phase == phases - 2)//jeśli półfinał
                    {
                        Match thirdPlace = _context.Matches.Include(m => m.TeamHome).Include(m => m.TeamAway)
                                        .Where(m => m.TournamentId == DTO.TournamentId && m.Phase == DTO.Phase + 1 && m.MatchId == 1)
                                        .FirstOrDefault();
                        //_context.Matches.Find(DTO.TournamentId, DTO.Phase + 1, 1)
                        //Team looser;
                        Team looser = DTO.HomeTeamPoints < DTO.AwayTeamPoints ? match.TeamHome.Team : match.TeamAway.Team;
                        /*Team looser = _context.Teams.Include(t => t.TeamInGroups)
                            .ThenInclude(tt => (DTO.HomeTeamPoints < DTO.AwayTeamPoints ? tt.MatchTeamHomes : tt.MatchTeamAways))
                            .Where(t => t.TeamInGroups.Any(tt => (DTO.HomeTeamPoints < DTO.AwayTeamPoints ? tt.MatchTeamHomes.Contains(match) : tt.MatchTeamAways.Contains(match))))
                            .FirstOrDefault();*/
                        /*if (DTO.HomeTeamPoints < DTO.AwayTeamPoints)
                        {
                            *//*looser = _context.TeamInGroups.Include(t => t.MatchTeamHomes).Include(t => t.Team)
                                .Where(t => t.MatchTeamHomes == match).Select(t => t.Team).FirstOrDefault();*//*
                            looser = qlooser.Where(t => t.TeamInGroups.Any(tt => tt.MatchTeamHomes == match)).FirstOrDefault();
                        }
                        else
                        {
                            *//*looser = _context.TeamInGroups.Include(t => t.MatchTeamAways).Include(t => t.Team)
                                .Where(t => t.MatchTeamAways == match).Select(t => t.Team).FirstOrDefault();*//*
                        }*/
                        if (thirdPlace.TeamHome.TeamId == null) thirdPlace.TeamHome.Team = looser;
                        else if (thirdPlace.TeamAway.TeamId == null) thirdPlace.TeamAway.Team = looser;
                        /*thirdPlace.TeamHome.Team = null;
                        thirdPlace.TeamAway.Team = null;*/
                        //else throw new Exception("Trying update next match, which already have both teams");
                        //TODO: Zastanowić się nad końcówką pozwalającą ręcznie zaktualizować zespoły na wypadek błędu podczas wpisywania
                        _context.Matches.Update(thirdPlace);
                    }
                }
            }
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
            return match;
        }
        public async Task<GetAvailableGroupMatchesDTO> GetAvailableGroupMatches(int tournamentId, MatchesEnum matchesEnum = MatchesEnum.All)
        {
            List<GetGroupsMatchesDTO> Groups = await _context.Groups.Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                            .Select(s => new GetGroupsMatchesDTO() { GroupName = s.Name, GroupId = s.Id })
                            .ToListAsync();
            Groups.ForEach(group =>
            {
                IQueryable<Match> query = _context.Matches.Include(m => m.TeamAway).ThenInclude(t => t.Team).Include(m => m.TeamHome).ThenInclude(t => t.Team)
                            .Where(m => m.TeamAway.GroupId == group.GroupId || m.TeamHome.GroupId == group.GroupId);
                switch (matchesEnum)
                {
                    case IMatchService.MatchesEnum.NotPlayed:
                        query = query.Where(m => m.TeamAwayScore == null && m.TeamHomeScore == null);
                        break;
                    case IMatchService.MatchesEnum.Played:
                        query = query.Where(m => m.TeamAwayScore != null && m.TeamHomeScore != null);
                        break;
                    case IMatchService.MatchesEnum.All:
                        break;
                }
                //List<GetMatchDTO> Matches = query.Select(m => new GetMatchDTO()
                var Matches = query.Select(m => new GetMatchDTO()
                {
                    TournamentId = m.TournamentId,
                    Phase = m.Phase,
                    MatchId = m.MatchId,
                    TeamHome = new DTOs.TeamDTO() { Name = m.TeamHome.Team.Name },
                    TeamAway = new DTOs.TeamDTO() { Name = m.TeamAway.Team.Name },
                    TeamHomeScore = m.TeamHomeScore,
                    TeamAwayScore = m.TeamAwayScore
                }).ToListAsync();

                /*List<GetMatchDTO> Matches = _context.Matches.Include(m => m.TeamAway).Include(m => m.TeamHome)
                            .Where(m => m.TeamAway.GroupId == group.GroupId || m.TeamHome.GroupId == group.GroupId)
                            .Select(m => new GetMatchDTO() {TeamHome= new DTOs.TeamDTO() {Name= m.TeamHome.Team.Name },TeamAway= new DTOs.TeamDTO() { Name = m.TeamAway.Team.Name } })
                            .ToList();*/
                group.Matches = Matches.Result;
            });

            GetAvailableGroupMatchesDTO result = new() { GroupsMatches = Groups };
            return result;
        }
        public async Task<GetLadderDTO> GetLadderMatches(int tournamentId)
        {
            List<GetPhasesWithMatchesDTO> Phases = await _context.Matches.GroupBy(m => m.Phase)
                            .Where(m => m.Key != 0)
                            .Select(m => new GetPhasesWithMatchesDTO() { Phase = m.Key })
                            .ToListAsync();
            Phases.ForEach(phase =>
            {
                phase.Matches = _context.Matches.Include(m => m.TeamAway).ThenInclude(t => t.Team)
                                .Include(m => m.TeamHome).ThenInclude(t => t.Team)
                                .Where(m => m.Phase == phase.Phase)
                                .Select(m => new GetMatchDTO()
                                {
                                    TournamentId = m.TournamentId,
                                    Phase = m.Phase,
                                    MatchId = m.MatchId,
                                    TeamHome = new DTOs.TeamDTO() { Name = m.TeamHome.Team.Name },
                                    TeamAway = new DTOs.TeamDTO() { Name = m.TeamAway.Team.Name },
                                    TeamHomeScore = m.TeamHomeScore,
                                    TeamAwayScore = m.TeamAwayScore
                                }).ToList();
            });
            GetLadderDTO result = new()
            {
                Phases = Phases
            };
            return result;

        }
    }
}
