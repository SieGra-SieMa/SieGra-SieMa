using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.TournamentDTO;
using SieGraSieMa.Models;

namespace SieGraSieMa.Services.Tournaments
{
    public interface ITournamentsService
    {
        public Task<IEnumerable<Tournament>> GetTournaments();

        public Task<Tournament> GetTournament(int id);

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

        public async Task<Tournament> GetTournament(int id)
        {
            var tournament = await _SieGraSieMaContext.Tournaments
                .Include(t => t.TeamInTournaments)
                .Include(t => t.Groups)
                .Include(t => t.Contests)
                .Include(t => t.Albums)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            return tournament;
        }

        public async Task<IEnumerable<Tournament>> GetTournaments()
        {
            var tournaments =  await _SieGraSieMaContext.Tournaments
                .Include(t => t.TeamInTournaments)
                .Include(t => t.Groups)
                .Include(t => t.Contests)
                .Include(t => t.Albums)
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
