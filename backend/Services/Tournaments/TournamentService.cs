using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.GroupDTO;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.DTOs.TeamInTournamentDTO;
using SieGraSieMa.DTOs.TournamentDTO;
using SieGraSieMa.Models;

namespace SieGraSieMa.Services.Tournaments
{
    public interface ITournamentsService
    {
        public Task<IEnumerable<ResponseTournamentDTO>> GetTournaments();

        public Task<ResponseTournamentDTO> GetTournament(int id);

        public Task<bool> CreateTournament(Tournament tournament);

        public Task<bool> UpdateTournament(int id, Tournament tournament);

        public Task<bool> DeleteTournament(int id);

    }
    public class TournamentService : ITournamentsService
    {

        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public TournamentService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }

        public async Task<bool> CreateTournament(Tournament tournament)
        {
            await _SieGraSieMaContext.Tournaments.AddAsync(tournament);
            if(await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteTournament(int id)
        {
            var tournament = await _SieGraSieMaContext.Tournaments.FindAsync(id);
            _SieGraSieMaContext.Tournaments.Remove(tournament);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<ResponseTournamentDTO> GetTournament(int id)
        {
            var tournament = await _SieGraSieMaContext.Tournaments
                .Include(t => t.TeamInTournaments)
                .Include(t => t.Groups)
                .Include(t => t.Contests)
                .Include(t => t.Albums)
                .Where(t => t.Id == id)
                .Select(t => new ResponseTournamentDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Description = t.Description,
                    Address = t.Address,
                    Albums = t.Albums.Select(a => new ResponseAlbumDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                        CreateDate = a.CreateDate,
                        TournamentId = a.TournamentId,
                        Media = a.Media.Select(m => new ResponseMediumDTO { Id = m.Id, AlbumId = m.AlbumId, Url = m.Url })
                    }),
                    Groups = t.Groups.Select(g => new ResponseGroupDTO { Id = g.Id, Name = g.Name, TournamentId = g.TournamentId, Ladder = g.Ladder }),
                    TeamInTournaments = t.TeamInTournaments.Select(i => new ResponseTeamInTournamentDTO { TeamId = i.TeamId, TournamentId = i.TournamentId, Paid = i.Paid })
                })
                .FirstOrDefaultAsync();

            return tournament;
        }

        public async Task<IEnumerable<ResponseTournamentDTO>> GetTournaments()
        {
            var tournaments =  await _SieGraSieMaContext.Tournaments
                .Include(t => t.TeamInTournaments)
                .Include(t => t.Groups)
                .Include(t => t.Contests)
                .Include(t => t.Albums)
                .Select(t => new ResponseTournamentDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Description = t.Description,
                    Address = t.Address,
                    Albums = t.Albums.Select(a => new ResponseAlbumDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                        CreateDate = a.CreateDate,
                        TournamentId = a.TournamentId,
                        Media = a.Media.Select(m => new ResponseMediumDTO { Id = m.Id, AlbumId = m.AlbumId, Url = m.Url })
                    }),
                    Groups = t.Groups.Select(g => new ResponseGroupDTO { Id = g.Id, Name = g.Name, TournamentId = g.TournamentId, Ladder = g.Ladder }),
                    TeamInTournaments = t.TeamInTournaments.Select(i => new ResponseTeamInTournamentDTO { TeamId = i.TeamId, TournamentId = i.TournamentId, Paid = i.Paid })
                })
                .ToListAsync();

            return tournaments;
        }

        public async Task<bool> UpdateTournament(int id, Tournament tournament)
        {
            var oldTournament = await _SieGraSieMaContext.Tournaments.FindAsync(id);
            if (oldTournament == null)
                return false;
            _SieGraSieMaContext.Tournaments.Update(tournament);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}
