using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.Users;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface IUserService
    {
        void AddUser(User User);
        UserDTO UpdateUser(string email, UserDetailsDTO userDetailsDTO);
        void DeleteUser(int Id);
        User GetUser(int Id);
        User GetUser(string Email);
        IEnumerable<User> GetUsers();
        public void JoinNewsletter(int userId);
        public void LeaveNewsletter(int userId);
        public Task<IEnumerable<User>> GetNewsletterSubscribers(int? id);
        public Task PreparingUserToBlock(int Id);
    }
    public class UserService : IUserService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public UserService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }
        public void AddUser(User User)
        {
            _SieGraSieMaContext.Users.Add(User);
            _SieGraSieMaContext.SaveChanges();
        }

        public async Task PreparingUserToBlock(int Id)
        {
            var teams = await _SieGraSieMaContext.Teams.Include(e => e.Players).ThenInclude(e => e.User).Where(e => e.CaptainId == Id).ToListAsync();
            if (teams.Any())
            {
                teams.ForEach(t =>
                {
                    if (t.Players.Count > 1) t.CaptainId = t.Players.Where(p => p.UserId != Id).Select(p => p.UserId).First();
                    else t.CaptainId = null;
                });
                _SieGraSieMaContext.UpdateRange(teams);
            }
            var newsletter = await _SieGraSieMaContext.Newsletters.Where(n => n.UserId == Id).FirstOrDefaultAsync();
            if (newsletter != null) _SieGraSieMaContext.Newsletters.Remove(newsletter);

            var players = await _SieGraSieMaContext.Players.Where(n => n.UserId == Id).ToListAsync();
            if (players.Any()) _SieGraSieMaContext.Players.RemoveRange(players);

            var token = await _SieGraSieMaContext.RefreshTokens.Where(n => n.UserId == Id).ToListAsync();
            if (token.Any()) _SieGraSieMaContext.RefreshTokens.RemoveRange(token);

            await _SieGraSieMaContext.SaveChangesAsync();
        }

        public void DeleteUser(int Id)
        {
            _SieGraSieMaContext.Users.Remove(GetUser(Id));
            _SieGraSieMaContext.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetNewsletterSubscribers(int? id)
        {
            if (id == null)
                return await _SieGraSieMaContext.Newsletters.Include(n => n.User).Select(n => n.User).ToListAsync();

            return await _SieGraSieMaContext.Newsletters.Include(n => n.User)
                .ThenInclude(u => u.Teams)
                .ThenInclude(u => u.TeamInTournaments)
                .Where(u => u.User.Teams.Any(t => t.TeamInTournaments.Any(t => t.TournamentId == id))).Select(n => n.User).ToListAsync();
        }

        public User GetUser(int Id)
        {
            return _SieGraSieMaContext.Users.Where(t => t.Id == Id).SingleOrDefault();
        }

        public User GetUser(string Email)
        {
            return _SieGraSieMaContext.Users.Where(t => t.Email == Email).SingleOrDefault();
        }
        public IEnumerable<User> GetUsers()
        {
            return _SieGraSieMaContext.Users.ToList();
        }

        public void JoinNewsletter(int userId)
        {
            var currentNewsletter = _SieGraSieMaContext.Newsletters.SingleOrDefault(n => n.UserId == userId);
            if (currentNewsletter == null)
                throw new Exception("User is already subscribed to newsletter");
            _SieGraSieMaContext.Newsletters.Add(new Newsletter { UserId = userId });
            _SieGraSieMaContext.SaveChanges();
        }

        public void LeaveNewsletter(int userId)
        {
            var currentNewsletter = _SieGraSieMaContext.Newsletters.SingleOrDefault(n => n.UserId == userId);
            if (currentNewsletter == null)
                throw new Exception("User currently not in any newsletter");

            _SieGraSieMaContext.Newsletters.Remove(currentNewsletter);
            _SieGraSieMaContext.SaveChanges();
        }

        public UserDTO UpdateUser(string email, UserDetailsDTO userDetails)
        {
            var user = GetUser(email);
            user.Name = userDetails.Name;
            user.Surname = userDetails.Surname;
            _SieGraSieMaContext.Users.Update(user);
            _SieGraSieMaContext.SaveChanges();
            return new UserDTO { Id = user.Id, Email = user.Email, Name = user.Name, Surname = user.Surname };
        }
    }
}
