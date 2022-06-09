using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface ILogService
    {
        public Task AddLog(Log log);
        //public Task<IEnumerable<Log>> GetLogs(int? id=null);
    }
    public class LogService : ILogService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public LogService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }

        public async Task AddLog(Log log)
        {
            await _SieGraSieMaContext.Logs.AddAsync(log);
            await _SieGraSieMaContext.SaveChangesAsync();
        }

        /*public async Task<IEnumerable<Log>> GetLogs(int? id = null)
        {
            return _SieGraSieMaContext.Logs.TakeLast(id.Value).ToList();
        }*/
    }
}
