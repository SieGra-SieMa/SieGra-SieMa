using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Interfaces
{
    public interface IPlayerService
    {
        void AddPlayer(Player Player);
        void RemovePlayer(int TeamId, int UserId);
        Player GetPlayer(int TeamId, int UserId);
        IEnumerable<Player> GetPlayers();
    }
}
