/*using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public PlayerService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }

        public Player GetPlayer(int TeamId, int UserId)
        {
            return _SieGraSieMaContext.Player.Where(p => p.TeamId == TeamId && p.UserId==UserId).SingleOrDefault();
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _SieGraSieMaContext.Player.ToList();
        }

        public void RemovePlayer(int TeamId, int UserId)
        {
            _SieGraSieMaContext.Player.Remove(GetPlayer(TeamId, UserId));
            _SieGraSieMaContext.SaveChanges();
        }
    }
}
*/