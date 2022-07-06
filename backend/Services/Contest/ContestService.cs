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
        public Task<ResponseContestDTO> CreateContest(Contest newContest);
        public Task<ResponseContestDTO> UpdateContest(int id, Contest newContest);
        public Task<ResponseContestDTO> DeleteContest(int id);
        public Task<ICollection<ResponseContestDTO>> GetContests(int tournamentId);
        public Task<ResponseContestDTO> GetContest(int id);
        public Task<ResponseContestantDTO> SetScore(int id, AddContestantDTO addContestantDTO);
        public Task<ResponseContestantDTO> DeleteScore(int id, AddContestantDTO addContestantDTO);
    }
    public class ContestService : IContestService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public ContestService(SieGraSieMaContext sieGraSieMaContext)
        {
            _SieGraSieMaContext = sieGraSieMaContext;
        }

        public async Task<ResponseContestDTO> CreateContest(Contest newContest)
        {
            await _SieGraSieMaContext.Contests.AddAsync(newContest);
            if (await _SieGraSieMaContext.SaveChangesAsync() == 0) throw new Exception("Nie można dodać konkursu");
            var contest = _SieGraSieMaContext.Contests.Where(c => c.TournamentId == newContest.TournamentId && c.Name == newContest.Name).FirstOrDefault();
            var result = new ResponseContestDTO
            {
                Id = contest.Id,
                Name = contest.Name,
                TournamentId = contest.TournamentId,
                Contestants = new List<ResponseContestantDTO>()
                /*Contestants = await _SieGraSieMaContext.Contestants.Include(c => c.User).Where(c => c.ContestId == id).Select(c => new ResponseContestantDTO
                {
                    Name = c.User.Name,
                    Surname = c.User.Surname,
                    Points = c.Points
                }).ToListAsync()*/
            };
            return result;
        }
        public async Task<ResponseContestDTO> UpdateContest(int id, Contest newContest)
        {
            var oldContest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (oldContest == null) throw new Exception("Nie ma konkursu o takim numerze");
            oldContest.Name = newContest.Name;
            _SieGraSieMaContext.Contests.Update(oldContest);
            if (await _SieGraSieMaContext.SaveChangesAsync() == 0) throw new Exception("Nie można zaktualizować konkursu");
            var result = new ResponseContestDTO
            {
                Id = oldContest.Id,
                Name = oldContest.Name,
                TournamentId = oldContest.TournamentId,
                Contestants = await _SieGraSieMaContext.Contestants.Include(c => c.User).Where(c => c.ContestId == id).Select(c => new ResponseContestantDTO
                {
                    Name = c.User.Name,
                    Surname = c.User.Surname,
                    Points = c.Points
                }).ToListAsync()
            };
            return result;
        }
        public async Task<ResponseContestDTO> DeleteContest(int id)
        {
            var contest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (contest == null) throw new Exception("Nie ma konkursu o takim numerze");
            if (_SieGraSieMaContext.Contestants.Where(c => c.ContestId == id).Any()) throw new Exception("Nie można usunąć konkursu z uczestnikami");
            _SieGraSieMaContext.Contests.Remove(contest);
            await _SieGraSieMaContext.SaveChangesAsync();
            var result = new ResponseContestDTO
            {
                Id = contest.Id,
                Name = contest.Name,
                TournamentId = contest.TournamentId,
                /*Contestants = await _SieGraSieMaContext.Contestants.Include(c => c.User).Where(c => c.ContestId == id).Select(c => new ResponseContestantDTO
                {
                    Name = c.User.Name,
                    Surname = c.User.Surname,
                    Points = c.Points
                }).ToListAsync()*/
            };
            return result;
        }
        public async Task<ResponseContestDTO> GetContest(int id)
        {
            var contest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (contest == null) throw new Exception("Nie ma konkursu o takim numerze");
            var result = new ResponseContestDTO
            {
                Id = contest.Id,
                Name = contest.Name,
                TournamentId = contest.TournamentId,
                Contestants = await _SieGraSieMaContext.Contestants.Include(c => c.User).Where(c => c.ContestId == id).Select(c => new ResponseContestantDTO
                {
                    //ContestId = c.ContestId,
                    Name = c.User.Name,
                    Surname = c.User.Surname,
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
        public async Task<ResponseContestantDTO> SetScore(int id, AddContestantDTO addContestantDTO)
        {
            User user = await _SieGraSieMaContext.Users.Where(t => t.Email == addContestantDTO.Email).FirstOrDefaultAsync();
            if (user == null) throw new Exception("Użytkownik z takim mailem nie istnieje");
            Contest contest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (contest == null) throw new Exception("Nie ma konkursu o takim numerze");
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

            await _SieGraSieMaContext.SaveChangesAsync();
            var result = new ResponseContestantDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Points = contestant.Points,
                UserId = user.Id
            };
            return result;
        }
        public async Task<ResponseContestantDTO> DeleteScore(int id, AddContestantDTO addContestantDTO)
        {
            User user = await _SieGraSieMaContext.Users.Where(t => t.Email == addContestantDTO.Email).FirstOrDefaultAsync();
            if (user == null) throw new Exception("Użytkownik z takim mailem nie istnieje");
            Contest contest = await _SieGraSieMaContext.Contests.FindAsync(id);
            if (contest == null) throw new Exception("Nie ma konkursu o takim numerze");
            Contestant contestant = await _SieGraSieMaContext.Contestants.FindAsync(id, user.Id);
            if (contestant == null) throw new Exception("Ten wynik nie może zostać usunięty, ponieważ nie istnieje");
            else
            {
                _SieGraSieMaContext.Contestants.Remove(contestant);
                contestant.Points = 0;
            }

            await _SieGraSieMaContext.SaveChangesAsync();
            var result = new ResponseContestantDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Points = contestant.Points,
                UserId = user.Id
            };
            return result;
        }
    }
}
