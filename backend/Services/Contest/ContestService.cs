using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.ContestantDTO;
using SieGraSieMa.DTOs.ContestDTO;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface IContestService
    {
        public Task<bool> CreateContest(Contest newContest);
        public Task<bool> UpdateContest(int id, Contest newContest);
        public Task<bool> DeleteContest(int id);
        public Task<ICollection<ResponseContestDTO>> GetContests(int tournamentId);
        public Task<ResponseContestDTO> GetContest(int id);
        public Task<bool> SetScore(int id, AddContestantDTO addContestantDTO);
    }
    public class ContestService : IContestService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public ContestService(SieGraSieMaContext sieGraSieMaContext)
        {
            _SieGraSieMaContext = sieGraSieMaContext;
        }

        public async Task<bool> CreateContest(Contest newContest)
        {
            await _SieGraSieMaContext.Contests.AddAsync(newContest);
            return await _SieGraSieMaContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateContest(int id, Contest newContest)
        {
            var oldContest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (oldContest == null) throw new Exception("There is no contest with this id");
            oldContest.Name = newContest.Name;
            _SieGraSieMaContext.Contests.Update(oldContest);
            return await _SieGraSieMaContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteContest(int id)
        {
            var contest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (contest == null) throw new Exception("There is no contest with this id");
            if (_SieGraSieMaContext.Contestants.Where(c => c.ContestId == id).Any()) throw new Exception("Cant delete contest with contestans");
            _SieGraSieMaContext.Contests.Remove(contest);
            return (await _SieGraSieMaContext.SaveChangesAsync() > 0);
        }
        public async Task<ResponseContestDTO> GetContest(int id)
        {
            var contest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (contest == null) throw new Exception("No contest with this id");
            var result = new ResponseContestDTO
            {
                Id = contest.Id,
                Name = contest.Name,
                TournamentId = contest.TournamentId,
                Contestants = await _SieGraSieMaContext.Contestants.Include(c=>c.User).Where(c => c.ContestId == id).Select(c => new ResponseContestantDTO
                {
                    //ContestId = c.ContestId,
                    Name=c.User.Name,
                    Surname=c.User.Surname,
                    //UserId = c.UserId,
                    Points = c.Points
                }).ToListAsync()
            };
            return result;
        }
        public async Task<ICollection<ResponseContestDTO>> GetContests(int tournamentId)
        {

            var result = _SieGraSieMaContext.Contests.Include(c => c.Contestants).ThenInclude(c => c.User).Where(c => c.TournamentId == tournamentId).Select(c =>
                new ResponseContestDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    TournamentId = c.TournamentId,
                    Contestants = c.Contestants.Select(cc => new ResponseContestantDTO
                    {
                        //ContestId = cc.ContestId,
                        Name = cc.User.Name,
                        Surname = cc.User.Surname,
                        //UserId = cc.UserId,
                        Points = cc.Points
                    }).ToList()
                }).ToListAsync();
            return await result;
        }
        public async Task<bool> SetScore(int id, AddContestantDTO addContestantDTO)
        {
            User user = await _SieGraSieMaContext.Users.Where(t => t.Email == addContestantDTO.Email).FirstOrDefaultAsync();
            if (user == null) throw new Exception("User with that email does not exist");
            Contest contest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (contest == null) throw new Exception("Contest with that id does not exist");
            Contestant contestant = await _SieGraSieMaContext.Contestants.FindAsync(id, user.Id);
            if (contestant == null)
            {
                contestant = new Contestant
                {
                    User = user,
                    Points = addContestantDTO.Points,
                    ContestId = id
                };
                await _SieGraSieMaContext.Contestants.AddAsync(contestant);
            }
            else
            {
                contestant.Points = addContestantDTO.Points;
                _SieGraSieMaContext.Contestants.Update(contestant);
            }
            
            return await _SieGraSieMaContext.SaveChangesAsync() > 0;
        }
    }
}
