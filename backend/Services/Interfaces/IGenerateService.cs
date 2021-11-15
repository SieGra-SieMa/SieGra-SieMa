using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Interfaces
{
    public interface IGenerateService
    {
        IEnumerable<Team> GenerateTeams(int amount, int tournamentId);
    }
}
